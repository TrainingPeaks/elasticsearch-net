﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Extras.FakeItEasy;
using ES.Net.Connection;
using ES.Net.Connection.Configuration;
using ES.Net.ConnectionPool;
using ES.Net.Exceptions;
using ES.Net.Providers;
using ES.Net.Serialization;
using ES.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace ES.Net.Tests.Unit.ConnectionPools
{
	[TestFixture]
	public class StaticConnectionPoolTests
	{
		private static Uri[] _uris = new[]
		{
			new Uri("http://localhost:9200"),
			new Uri("http://localhost:9201"),
			new Uri("http://localhost:9202"),
			new Uri("http://localhost:9203"),
		};
		private static readonly int _retries = _uris.Count() - 1;
		private readonly StaticConnectionPool _connectionPool;
		private readonly ConnectionConfiguration _config;
		private ElasticsearchResponse<Stream> _ok;
		private ElasticsearchResponse<Stream> _bad;

		public StaticConnectionPoolTests()
		{
			_connectionPool = new StaticConnectionPool(_uris);
			_config = new ConnectionConfiguration(_connectionPool);

			_ok = FakeResponse.Ok(_config);
			_bad = FakeResponse.Bad(_config);
		}

		private ITransport ProvideTransport(AutoFake fake)
		{
			var dateTimeParam = new TypedParameter(typeof(IDateTimeProvider), null);
			var memoryStreamParam = new TypedParameter(typeof(IMemoryStreamProvider), null);
			var serializerParam = new TypedParameter(typeof(IElasticsearchSerializer), null);
			return fake.Provide<ITransport, Transport>(dateTimeParam, serializerParam, memoryStreamParam);
		}

		[Test]
		public void ThrowsOutOfNodesException_AndRetriesTheSpecifiedTimes()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//set up connection configuration that holds a connection pool
				//with '_uris' (see the constructor)
				fake.Provide<IConnectionConfigurationValues>(_config);
				//prove a real HttpTransport with its unspecified dependencies
				//as fakes
				this.ProvideTransport(fake);

				//set up fake for a call on IConnection.GetSync so that it always throws 
				//an exception
				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Throws<Exception>();
				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(_ok);
				
				//create a real ElasticsearchClient with it unspecified dependencies
				//as fakes
				var client = fake.Resolve<ElasticsearchClient>();

				//we don't specify our own value so it should be up to the connection pool
				client.Settings.MaxRetries.Should().Be(null);
				
				//the default for the connection pool should be the number of nodes - 1;
				client.Settings.ConnectionPool.MaxRetries.Should().Be(_retries);

				//calling GET / should throw an OutOfNodesException because our fake
				//will always throw an exception
				Assert.Throws<MaxRetryException>(()=> client.Info());
				//the GetSync method must in total have been called the number of nodes times.
				getCall.MustHaveHappened(Repeated.Exactly.Times(_retries + 1));

			}
		}

		[Test]
		public void AllNodesMustBeTriedOnce()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//set up client with fakes
				fake.Provide<IConnectionConfigurationValues>(_config);
				var connection = fake.Resolve<NoopConnection>();
				fake.Provide<IConnection>(connection);
				this.ProvideTransport(fake);

				//provide a unique fake for each node.
				var calls = _uris.Select(u =>
					A.CallTo(()=> fake.Resolve<IUriObserver>()
						.Observe(A<Uri>.That.Matches(uu=>uu.Port == u.Port)))
				).ToList();

				//all of our fakes throw an exception
				foreach (var c in calls)
					c.Throws<Exception>();

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.Throws<MaxRetryException>(()=> client.Info());
				
				//make sure we've observed an attempt on all the nodes
				foreach (var call in calls)
					call.MustHaveHappened(Repeated.Exactly.Once);

			}
		}
		
		[Test]
		public void HardRetryLimitTakesPrecedenceOverNumberOfNodes()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//setup config values with a hight retry count
				fake.Provide<IConnectionConfigurationValues>(
					new ConnectionConfiguration(_connectionPool)
						.MaximumRetries(7)
				);
				var getCall = A.CallTo(() => 
					fake.Resolve<IConnection>().GetSync(A<Uri>._, A<IRequestConfiguration>._));
				getCall.Throws<Exception>();

				var transport = this.ProvideTransport(fake);
				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(_ok);

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.Throws<MaxRetryException>(()=> client.Info());
				
				//We want to see 8 attempt to do a GET on /
				//(original call + retry of 7 == 8)
				getCall.MustHaveHappened(Repeated.Exactly.Times(8));

			}
		}

		[Test]
		public void AConnectionMustBeMadeEvenIfAllNodesAreDead()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//make sure we retry one more time then we have nodes
				//original call + 4 retries == 5
				fake.Provide<IConnectionConfigurationValues>(
					new ConnectionConfiguration(_connectionPool)
						.MaximumRetries(4)
				);
				//set up our GET to / to return 4 503's followed by a 200
				var getCall = A.CallTo(() => fake.Resolve<IConnection>().GetSync(A<Uri>._, A<IRequestConfiguration>._));
				getCall.ReturnsNextFromSequence(
					_ok
				);
				var transport = this.ProvideTransport(fake);
				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.ReturnsNextFromSequence(
					_bad,
					_bad,
					_bad,
					_ok
				);
				//setup client
				var client = fake.Resolve<ElasticsearchClient>();
				
				//Do not throw because by miracle the 4th retry manages to give back a 200
				//even if all nodes have been marked dead.
				Assert.DoesNotThrow(()=> client.Info());

				//original call + 4 retries == 5
				getCall.MustHaveHappened(Repeated.Exactly.Once);
				//ping must have been send out 4 times to the 4 nodes being used for the first time
				pingCall.MustHaveHappened(Repeated.Exactly.Times(4));

			}
		}

		[Test]
		public void AllNodesWillBeMarkedDead()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//set up a fake datetimeprovider
				var dateTimeProvider = fake.Resolve<IDateTimeProvider>();
				fake.Provide(dateTimeProvider);
				//create a connectionpool that uses the fake datetimeprovider
				var connectionPool = new StaticConnectionPool(
					_uris, 
					dateTimeProvider: dateTimeProvider
				);
				var config = new ConnectionConfiguration(connectionPool);
				fake.Provide<IConnectionConfigurationValues>(config);
				
				//Now() on the fake still means Now()
				A.CallTo(()=>dateTimeProvider.Now()).Returns(DateTime.UtcNow);
				//Set up individual mocks for each DeadTime(Uri uri,...) call
				//where uri matches one of the node ports
				var calls = _uris.Select(u =>
					A.CallTo(()=> dateTimeProvider.DeadTime(
						A<Uri>.That.Matches(uu=>uu.Port == u.Port),
						A<int>._, A<int?>._, A<int?>._
					))).ToList();
		
				//all the fake mark dead calls return 60 seconds into the future
				foreach (var call in calls)
					call.Returns(DateTime.UtcNow.AddSeconds(60));
				
				//When we do a GET on / we always recieve a 503
				var getCall = A.CallTo(() => fake.Resolve<IConnection>().GetSync(A<Uri>._, A<IRequestConfiguration>._));
				getCall.Returns(_bad);
				
				var transport = this.ProvideTransport(fake);
				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(_ok);
				var client = fake.Resolve<ElasticsearchClient>();
				
				//Since we always get a 503 we should see an out of nodes exception
				Assert.Throws<MaxRetryException>(()=> client.Info());

				pingCall.MustHaveHappened(Repeated.Exactly.Times(4));

				//The call should be tried on all the nodes
				getCall.MustHaveHappened(Repeated.Exactly.Times(4));

				//We should see each individual node being marked as dead
				foreach (var call in calls)
					call.MustHaveHappened(Repeated.Exactly.Once);
			}
		}
		
		[Test]
		public void IfAConnectionComesBackToLifeOnItsOwnItShouldBeMarked()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//Setting up a datetime provider so that can track dead/alive marks
				var dateTimeProvider = fake.Resolve<IDateTimeProvider>();
				A.CallTo(() => dateTimeProvider.Now()).Returns(DateTime.UtcNow);
				var markDeadCall = A.CallTo(() => dateTimeProvider.DeadTime(A<Uri>._, A<int>._, A<int?>._, A<int?>._));
				var markAliveCall = A.CallTo(() => dateTimeProvider.AliveTime(A<Uri>._, A<int>._));
				markDeadCall.Returns(DateTime.UtcNow.AddSeconds(60));
				markAliveCall.Returns(new DateTime());
				fake.Provide(dateTimeProvider);
				var connectionPool = new StaticConnectionPool(
					_uris, 
					dateTimeProvider: dateTimeProvider);

				//set retries to 4 
				fake.Provide<IConnectionConfigurationValues>(
					new ConnectionConfiguration(connectionPool)
						.MaximumRetries(4) 
				);

				//fake getsync handler that return a 503 4 times and then a 200
				//this will cause all 4 nodes to be marked dead on the first client call
				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.ReturnsNextFromSequence(
					_bad,
					_bad,
					_bad,
					_bad,
					_ok
				);
				//provide a transport with all the dependencies resolved
				var transport = this.ProvideTransport(fake);
				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(_ok);

				//instantiate connection with faked dependencies
				var client = fake.Resolve<ElasticsearchClient>();
				
				//Do not throw because by miracle the 4th retry manages to give back a 200
				//even if all nodes have been marked dead.
				Assert.DoesNotThrow(()=> client.Info());

				//original call + 4 retries is 5
				getCall.MustHaveHappened(Repeated.Exactly.Times(5));
				//4 nodes must be marked dead
				markDeadCall.MustHaveHappened(Repeated.Exactly.Times(4));
				//atleast one of them sprung back to live so markAlive must be called once
				markAliveCall.MustHaveHappened(Repeated.Exactly.Times(1));

			}
		}
		
		[Test]
		public void IfAllButOneConnectionDiesSubsequentRequestsMustUseTheOneAliveConnection()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				//Setting up a datetime provider so that we can measure which
				//nodes have been marked alive and dead.
				//we provide a different fake for the method call with the last node
				//as argument.
				var dateTimeProvider = fake.Resolve<IDateTimeProvider>();
				A.CallTo(() => dateTimeProvider.Now()).Returns(DateTime.UtcNow);
				var markOthersDeadCall = A.CallTo(() => dateTimeProvider
					.DeadTime(A<Uri>.That.Not.Matches(u=>u.Port == 9203), A<int>._, A<int?>._, A<int?>._));
				var markLastDead = A.CallTo(() => dateTimeProvider
					.DeadTime(A<Uri>.That.Matches(u=>u.Port == 9203), A<int>._, A<int?>._, A<int?>._));
				var markOthersAliveCall = A.CallTo(() => dateTimeProvider
					.AliveTime(A<Uri>.That.Not.Matches(u=>u.Port == 9203), A<int>._));
				var markLastAlive = A.CallTo(() => dateTimeProvider
					.AliveTime(A<Uri>.That.Matches(u=>u.Port == 9203), A<int>._));
				markOthersDeadCall.Returns(DateTime.UtcNow.AddSeconds(60));
				markLastAlive.Returns(new DateTime());
				fake.Provide(dateTimeProvider);

				//Creating the connection pool making sure nodes are not randomized
				//So we are sure 9203 is that last node in the pool
				var connectionPool = new StaticConnectionPool(
					_uris, 
					randomizeOnStartup: false,
					dateTimeProvider: dateTimeProvider
				);
				var config = new ConnectionConfiguration(connectionPool);
				fake.Provide<IConnectionConfigurationValues>(config);

				// provide a simple fake for synchronous get
				var getCall = FakeCalls.GetSyncCall(fake); 

				//The first three tries get a 503 causing the first 3 nodes to be marked dead
				//all the subsequent requests should be handled by 9203 which gives a 200 4 times
				getCall.ReturnsNextFromSequence(
					FakeResponse.Bad(config),
					FakeResponse.Bad(config),
					FakeResponse.Bad(config),
					FakeResponse.Ok(config),
					FakeResponse.Ok(config),
					FakeResponse.Ok(config),
					FakeResponse.Ok(config)
				);
				//provide a transport with all the dependencies resolved
				var transport = this.ProvideTransport(fake);
				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(_ok);
				
				//instantiate connection with faked dependencies
				var client = fake.Resolve<ElasticsearchClient>();
				
				//We call the root for each node 4 times, eventhough the first 3 nodes
				//give back a timeout the default ammount of retries is 4 (each provided node)
				//and the last retry will hit our 9203 node.
				Assert.DoesNotThrow(()=> client.Info());
				//These calls should not hit the dead nodes and go straight to the active 9203
				Assert.DoesNotThrow(()=> client.Info());
				Assert.DoesNotThrow(()=> client.Info());
				Assert.DoesNotThrow(()=> client.Info());
				
				//The last node should never be marked dead
				markLastDead.MustNotHaveHappened();
				//the other nodes should never be marked alive
				markOthersAliveCall.MustNotHaveHappened();
				//marking the other 3 nodes dead should only happen once for each
				markOthersDeadCall.MustHaveHappened(Repeated.Exactly.Times(3));
				//every time a connection succeeds on a node it will be marked
				//alive therefor the last node should be marked alive 4 times
				markLastAlive.MustHaveHappened(Repeated.Exactly.Times(4));
			}
		}

		[Test]
		public void ShouldRetryOnPingConnectionException_Async()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{	
				var connectionPool = new StaticConnectionPool(_uris, randomizeOnStartup: false);
				var config = new ConnectionConfiguration(connectionPool);
				
				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingCall = FakeCalls.PingAtConnectionLevelAsync(fake);
				var seenPorts = new List<int>();
				pingCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					throw new Exception("Something bad happened");
				});

				var getCall = FakeCalls.GetCall(fake);
				getCall.Returns(FakeResponse.OkAsync(config));

				var client = fake.Resolve<ElasticsearchClient>();

				var e = Assert.Throws<MaxRetryException>(async () => await client.InfoAsync());
				pingCall.MustHaveHappened(Repeated.Exactly.Times(_retries + 1));
				getCall.MustNotHaveHappened();
				
				//make sure that if a ping throws an exception it wont
				//keep retrying to ping the same node but failover to the next
				seenPorts.ShouldAllBeEquivalentTo(_uris.Select(u=>u.Port));
				var ae = e.InnerException as AggregateException;
				ae = ae.Flatten();
				ae.InnerException.Message.Should().Contain("Pinging");
			}
		}
		
		[Test]
		public void ShouldRetryOnPingConnectionException()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{	
				var connectionPool = new StaticConnectionPool(_uris, randomizeOnStartup: false);
				var config = new ConnectionConfiguration(connectionPool);
				
				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				var seenPorts = new List<int>();
				pingCall.ReturnsLazily((Uri u, IRequestConfiguration c) =>
				{
					seenPorts.Add(u.Port);
					throw new Exception("Something bad happened");
				});

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Ok(config));

				var client = fake.Resolve<ElasticsearchClient>();

				var e = Assert.Throws<MaxRetryException>(() => client.Info());
				pingCall.MustHaveHappened(Repeated.Exactly.Times(_retries + 1));
				getCall.MustNotHaveHappened();
				
				//make sure that if a ping throws an exception it wont
				//keep retrying to ping the same node but failover to the next
				seenPorts.ShouldAllBeEquivalentTo(_uris.Select(u=>u.Port));
				var aggregateException = e.InnerException as AggregateException;
				aggregateException.Should().NotBeNull();
				aggregateException.InnerExceptions.Should().Contain(ex => ex.GetType().Name == "PingException");
			}
		}

		[Test]
		public void ShouldNotThrowAndNotRetry401()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var uris = new[]
				{
					new Uri("http://localhost:9200"),
					new Uri("http://localhost:9201"),
					new Uri("http://localhost:9202")
				};
				var connectionPool = new StaticConnectionPool(uris, randomizeOnStartup: false);
				var config = new ConnectionConfiguration(connectionPool);

				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Any(config, 401));

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.DoesNotThrow(() => client.Info());
				pingCall.MustHaveHappened(Repeated.Exactly.Once);
				getCall.MustHaveHappened(Repeated.Exactly.Once);
			}
		}

		[Test]
		public void ShouldNotThrowAndNotRetry401_Async()
		{
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var uris = new[]
				{
					new Uri("http://localhost:9200"),
					new Uri("http://localhost:9201"),
					new Uri("http://localhost:9202")
				};
				var connectionPool = new StaticConnectionPool(uris, randomizeOnStartup: false);
				var config = new ConnectionConfiguration(connectionPool);

				fake.Provide<IConnectionConfigurationValues>(config);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingAsyncCall = FakeCalls.PingAtConnectionLevelAsync(fake);
				pingAsyncCall.Returns(FakeResponse.OkAsync(config));

				//sniffing is always synchronous and in turn will issue synchronous pings
				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(config));

				var getCall = FakeCalls.GetCall(fake);
				getCall.Returns(FakeResponse.Any(config, 401));

				var client = fake.Resolve<ElasticsearchClient>();

				Assert.DoesNotThrow(async () => await client.InfoAsync());
				getCall.MustHaveHappened(Repeated.Exactly.Once);
			}
		}

		[Test]
		public void AllHttpsUris_DoesNotThrow()
		{
			var nodes = new Uri[]
			{
				new Uri("https://test1"),
				new Uri("https://test2")
			};
			Assert.DoesNotThrow(() => new StaticConnectionPool(nodes));
		}
		
		[Test]
		public void AllHttpsUris_UsingSsl_IsTrue()
		{
			var nodes = new Uri[]
			{
				new Uri("https://test1"),
				new Uri("https://test2")
			};
			Assert.IsTrue(new StaticConnectionPool(nodes).UsingSsl);
		}


		[Test]
		public void AllHttpUris_DoesNotThrow()
		{
			var nodes = new Uri[]
			{
				new Uri("http://test1"),
				new Uri("http://test2")
			};
			Assert.DoesNotThrow(() => new StaticConnectionPool(nodes));
		}

		[Test]
		public void AllHttpUris_UsingSsl_IsFalse()
		{
			var nodes = new Uri[]
			{
				new Uri("http://test1"),
				new Uri("http://test2")
			};
			Assert.IsFalse(new StaticConnectionPool(nodes).UsingSsl);
		}

		[Test]
		public void HttpAndHttpsUris_Throws()
		{
			var nodes = new Uri[]
			{
				new Uri("http://test1"),
				new Uri("https://test2")
			};
			Assert.Throws<ArgumentException>(() => new StaticConnectionPool(nodes));
		}
	}
}
