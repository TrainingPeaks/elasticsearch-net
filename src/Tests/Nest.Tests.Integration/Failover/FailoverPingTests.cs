﻿using System;
using ES.Net;
using ES.Net.ConnectionPool;
using FluentAssertions;
using NUnit.Framework;

namespace Nest17.Tests.Integration.Failover
{

	/// <summary>
	/// I use this class to answer questions on github issues/stackoverflow
	/// Tests that are written here are subject to removal at any time
	/// </summary>
	[TestFixture]
	public class FailoverPingTests 
	{
		[Test]
		public void FailoverShouldOnlyPingDeadNodes()
		{
			var seeds = new[]
			{
				ElasticsearchConfiguration.CreateBaseUri(9202),
				ElasticsearchConfiguration.CreateBaseUri(9201),
				ElasticsearchConfiguration.CreateBaseUri(9200)
			};
			var sniffingConnectionPool = new SniffingConnectionPool(seeds, randomizeOnStartup: false);
			var connectionSettings = new ConnectionSettings(sniffingConnectionPool);
			var client = new ElasticClient(connectionSettings);
			var rootNode = client.RootNodeInfo();
			var metrics = rootNode.ConnectionStatus.Metrics;
			
			//ping 9202 + 9201 + 9200 and call 9200
			metrics.Requests.Count.Should().Be(4);
			metrics.Requests[0].Node.Port.Should().Be(9202);
			metrics.Requests[0].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[0].EllapsedMilliseconds.Should().BeLessOrEqualTo(1100);
			metrics.Requests[1].Node.Port.Should().Be(9201);
			metrics.Requests[1].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[1].EllapsedMilliseconds.Should().BeLessOrEqualTo(1100);
			metrics.Requests[2].Node.Port.Should().Be(9200);
			metrics.Requests[2].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[2].EllapsedMilliseconds.Should().BeLessOrEqualTo(1100);
			metrics.Requests[3].Node.Port.Should().Be(9200);
			metrics.Requests[3].RequestType.Should().Be(RequestType.ElasticsearchCall);
			metrics.Requests[3].EllapsedMilliseconds.Should().BeLessOrEqualTo(1100);



			rootNode = client.RootNodeInfo();
			metrics = rootNode.ConnectionStatus.Metrics;
			metrics.Requests.Count.Should().Be(1);
			metrics.Requests[0].Node.Port.Should().Be(9200);
			metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);

			rootNode = client.RootNodeInfo();
			metrics = rootNode.ConnectionStatus.Metrics;
			metrics.Requests.Count.Should().Be(1);
			metrics.Requests[0].Node.Port.Should().Be(9200);
			metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);


			rootNode = client.RootNodeInfo();
			metrics = rootNode.ConnectionStatus.Metrics;
			metrics.Requests.Count.Should().Be(1);
			metrics.Requests[0].Node.Port.Should().Be(9200);
			metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);


			rootNode = client.RootNodeInfo();	
			metrics = rootNode.ConnectionStatus.Metrics;
			metrics.Requests.Count.Should().Be(1);
			metrics.Requests[0].Node.Port.Should().Be(9200);
			metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);


		}


		[Test]
		public async void FailoverShouldOnlyPingDeadNodes_Async()
		{
			var seeds = new[]
			{
				ElasticsearchConfiguration.CreateBaseUri(9202),
				ElasticsearchConfiguration.CreateBaseUri(9201),
				ElasticsearchConfiguration.CreateBaseUri(9200)
			};
			var sniffingConnectionPool = new SniffingConnectionPool(seeds, randomizeOnStartup: false);
			var connectionSettings = new ConnectionSettings(sniffingConnectionPool);
			var client = new ElasticClient(connectionSettings);
			var rootNode = await client.RootNodeInfoAsync();
			var metrics = rootNode.ConnectionStatus.Metrics;
			
			//ping 9202 + 9201 + 9200 and call 9200
			metrics.Requests.Count.Should().Be(4);
			metrics.Requests[0].Node.Port.Should().Be(9202);
			metrics.Requests[0].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[0].EllapsedMilliseconds.Should().BeLessOrEqualTo(1100);
			metrics.Requests[1].Node.Port.Should().Be(9201);
			metrics.Requests[1].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[1].EllapsedMilliseconds.Should().BeLessOrEqualTo(1100);
			metrics.Requests[2].Node.Port.Should().Be(9200);
			metrics.Requests[2].RequestType.Should().Be(RequestType.Ping);
			metrics.Requests[2].EllapsedMilliseconds.Should().BeLessOrEqualTo(1100);
			metrics.Requests[3].Node.Port.Should().Be(9200);
			metrics.Requests[3].RequestType.Should().Be(RequestType.ElasticsearchCall);
			metrics.Requests[3].EllapsedMilliseconds.Should().BeLessOrEqualTo(1100);

			rootNode = await client.RootNodeInfoAsync();
			metrics = rootNode.ConnectionStatus.Metrics;
			metrics.Requests.Count.Should().Be(1);
			metrics.Requests[0].Node.Port.Should().Be(9200);
			metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);

			rootNode = await client.RootNodeInfoAsync();
			metrics = rootNode.ConnectionStatus.Metrics;
			metrics.Requests.Count.Should().Be(1);
			metrics.Requests[0].Node.Port.Should().Be(9200);
			metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);

			rootNode = await client.RootNodeInfoAsync();
			metrics = rootNode.ConnectionStatus.Metrics;
			metrics.Requests.Count.Should().Be(1);
			metrics.Requests[0].Node.Port.Should().Be(9200);
			metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);

			rootNode = await client.RootNodeInfoAsync();
			metrics = rootNode.ConnectionStatus.Metrics;
			metrics.Requests.Count.Should().Be(1);
			metrics.Requests[0].Node.Port.Should().Be(9200);
			metrics.Requests[0].RequestType.Should().Be(RequestType.ElasticsearchCall);


		}
	}
}