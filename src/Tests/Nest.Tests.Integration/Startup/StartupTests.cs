﻿using System;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using System.Threading;
using AppDomainToolkit;
using ES.Net;
using ES.Net.Connection;
using FluentAssertions;
using Nest17;
using Nest17.Tests.Integration;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace StartupTests
{
	[TestFixture]
	public class StartupTests : IntegrationTests
	{
		private static readonly AppDomainSetup _setupInfo = new AppDomainSetup()
		{
			ApplicationName = "NEST.Warmup.Tests",
			ApplicationBase = Environment.CurrentDirectory,
			PrivateBinPath = Environment.CurrentDirectory
		};

		[Test]
		[Ignore]
		public void Calling_RootNodeInfo_TwiceInOneAssembly_IsFast()
		{
			using (var context = AppDomainContext.Create(_setupInfo))
			{
				var result = RemoteFunc.Invoke(context.Domain,
					() => new CallRoutine(c=>c.RootNodeInfo())
				);

				result.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(100);
				
				result = RemoteFunc.Invoke(context.Domain,
					() => new CallRoutine(c=>c.RootNodeInfo())
				);

				result.ElapsedMilliseconds.Should().BeLessOrEqualTo(5);
			}
		}
		
		[Test]
		[Ignore]
		public void Calling_RootNodeInfo_OnceInTwoAsemblies_SlowTwice()
		{
			using (var context = AppDomainContext.Create(_setupInfo))
			{
				var result = RemoteFunc.Invoke(context.Domain,
					() => new CallRoutine(c=>c.RootNodeInfo())
				);

				result.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(100);
			}
			
			using (var context = AppDomainContext.Create(_setupInfo))
			{
				var result = RemoteFunc.Invoke(context.Domain,
					() => new CallRoutine(c=>c.RootNodeInfo())
				);

				result.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(100);
			}
		}

		
		[Test]
		[Ignore]
		public void Calling_Search_TwiceInAppDomainIsFast()
		{
			using (var context = AppDomainContext.Create(_setupInfo))
			{
				var result = RemoteFunc.Invoke(context.Domain,
					() => new CallRoutine(c=>c.Search<ElasticsearchProject>(s=>s.MatchAll()))
				);

				result.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(100);
				
				result = RemoteFunc.Invoke(context.Domain,
					() => new CallRoutine(c=>c.Search<ElasticsearchProject>(s=>s.MatchAll()))
				);

				result.ElapsedMilliseconds.Should().BeLessOrEqualTo(10);
			}
		}
		
		[Test]
		[Ignore]
		public void Calling_Complex_TypedSearch_IsAlsoFaster_AfterBeingCalledOnce()
		{
			using (var context = AppDomainContext.Create(_setupInfo))
			{
				var result = RemoteFunc.Invoke(context.Domain,
					() => new CallRoutine(c=>c
						.Search<ElasticsearchProject>(s => s
							.Query(q => q.Term("field", "value") && q.Term(p => p.Name, "name"))
							.Filter(f => f.GeoPolygon(p => p.MyGeoShape, "1.0", "2.0", "3.0"))
							.Aggregations(a => a
								.Terms("term_items", gh => gh
									.Field(p => p.Content)
									.Aggregations(gha => gha
										.SignificantTerms("bucket_agg", m => m
											.Field(p => p.Content)
											.Size(2)
											.Aggregations(ma => ma.Terms("country", t => t.Field(p => p.Country)))
										)
									)
								)
							)
						)
					)
				);

				result.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(200);
				var againNoWarmup = RemoteFunc.Invoke(context.Domain,
					() => new CallRoutine(c=>c
						.Search<ElasticsearchProject>(s=>s
							.Query(q => q.Term("field", "value") && q.Term(p => p.Name, "name"))
							.Filter(f => f.GeoPolygon(p => p.MyGeoShape, "1.0", "2.0", "3.0"))
							.Aggregations(a => a
								.Terms("term_items", gh => gh
									.Field(p => p.Content)
									.Aggregations(gha => gha
										.SignificantTerms("bucket_agg", m => m
											.Field(p => p.Content)
											.Size(2)
											.Aggregations(ma => ma.Terms("country", t => t.Field(p => p.Country)))
										)
									)
								)
							)
					
						)
					)
				);

				againNoWarmup.ElapsedMilliseconds.Should().BeLessOrEqualTo(10);
			}
		}
	}





	[Serializable]
	public class BaseElasticsearchInSeparateDomain : MarshalByRefObject
	{
		protected readonly IElasticClient _client;
		public BaseElasticsearchInSeparateDomain()
		{
			var settings = Settings(9200);
			this._client = new ElasticClient(settings, new InMemoryConnection(settings));
		}

		private ConnectionSettings Settings(int? port = null, Uri hostOverride = null)
		{

			return new ConnectionSettings(hostOverride ?? CreateBaseUri(port), ElasticsearchConfiguration.DefaultIndex)
				.UsePrettyResponses()
				.ExposeRawResponse();
		}

		private Uri CreateBaseUri(int? port = null)
		{
			var host = "localhost";
			if (port == null && Process.GetProcessesByName("fiddler").HasAny())
				host = "ipv4.fiddler";

			var uri = new UriBuilder("http", host, port.GetValueOrDefault(9200)).Uri;
			return uri;
		}
	}


	//This runs in a separate appdomain
	[Serializable]
	public class CallRoutine : BaseElasticsearchInSeparateDomain
	{
		public long ElapsedMilliseconds { get; set; }

		public CallRoutine(Action<IElasticClient> call)
		{
			var sw = Stopwatch.StartNew();
			call(_client);
			sw.Stop();
			this.ElapsedMilliseconds = sw.ElapsedMilliseconds;
		}
	}

}