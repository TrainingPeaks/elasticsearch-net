﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ES.Net;
using NUnit.Framework;

namespace Nest17.Tests.Integration
{
	[TestFixture]
	public class ConnectionTests : IntegrationTests
	{
		[Test]
		public void TestSettings()
		{
			Assert.AreEqual(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex);
			Assert.AreEqual(this.Settings.MaximumAsyncConnections, ElasticsearchConfiguration.MaxConnections);
		}
		
		[Test]
		public void TestConnectSuccess()
		{
			var rootNodeInfo = this.Client.RootNodeInfo();
			Assert.True(rootNodeInfo.IsValid);
			Assert.True(rootNodeInfo.ConnectionStatus.Success);
		}
		[Test]
		public void Construct_Client_With_NullOrEmpty_Settings()
		{
			Assert.Throws<UriFormatException>(() =>
			{
				var settings = new ConnectionSettings(new Uri("http://:80"), "index");
			});
			Assert.Throws<UriFormatException>(() =>
			{
				var settings = new ConnectionSettings(new Uri(":asda:asdasd:80"), "index");
			});
		}
		[Test]
		public void Construct_Client_With_Invalid_Hostname()
		{
			Assert.Throws<UriFormatException>(() =>
			{
				var settings = new ConnectionSettings(new Uri("some mangled hostname:80"), "index");
			});

		}
		[Test]
		public void Connect_To_Unknown_Hostname()
		{
			IRootInfoResponse result = null;

			//this test will fail if fiddler is enabled since the proxy 
			//will report a statuscode of 502 instead of -1
			Assert.Throws<WebException>(() =>
			{
				var settings = new ConnectionSettings(new Uri("http://youdontownthis.domain.do.you"), "index");
				var client = new ElasticClient(settings);
				result = client.RootNodeInfo();
				Assert.False(result.IsValid);
				Assert.NotNull(result.ConnectionStatus);
			});
		
		}
		[Test]
		public void TestConnectSuccessWithUri()
		{
			var settings = new ConnectionSettings(ElasticsearchConfiguration.CreateBaseUri(), "index");
			var client = new ElasticClient(settings);
			var result = client.RootNodeInfo();

			Assert.True(result.IsValid);
			Assert.NotNull(result.ConnectionStatus.HttpStatusCode);
		}

		[Test]
		public void ConnectUsingRawClient()
		{
			var result = this.Client.Raw.Info();
			Assert.IsTrue(result.Success);
			StringAssert.EndsWith(":9200/?pretty=true", result.RequestUrl);


			var resultWithQueryString = this.Client.Raw.Info(qs => qs.AddQueryString("hello", "world"));
			Assert.IsTrue(resultWithQueryString.Success);

			StringAssert.EndsWith(":9200/?hello=world&pretty=true", resultWithQueryString.RequestUrl);
		}

		[Test]
		public void ConnectUsingRawClientComplexCall()
		{
			var result = this.Client.Raw.ClusterHealth(s => s
				.Level(Level.Indices)
				.Local(true)
				.WaitForActiveShards(1)
			);
			Assert.IsTrue(result.Success);
			StringAssert.EndsWith(":9200/_cluster/health?level=indices&local=true&wait_for_active_shards=1&pretty=true", result.RequestUrl);

		}
	}
}
