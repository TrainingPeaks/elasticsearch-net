﻿using ES.Net;
using ES.Net.Connection;
using NUnit.Framework;
using System;
using System.Collections.Specialized;

namespace Nest17.Tests.Unit.Domain.Connection
{
	using System.Net;

	[TestFixture]
	public class ConnectionTests : BaseJsonTests
	{
		[Test]
		public void CanCreateConnectionWithCustomQueryStringParameters()
		{
			// Arrange
			var uri = new Uri("http://localhost");
			var query = new NameValueCollection { { "authToken", "ABCDEFGHIJK" } };
			var connectionSettings = new ConnectionSettings(uri, "index").SetGlobalQueryStringParameters(query);
			var client = new ElasticClient(connectionSettings, new InMemoryConnection(connectionSettings));
			var result = client.RootNodeInfo();

			// Assert
			Assert.AreEqual(result.ConnectionStatus.RequestUrl, "http://localhost/?authToken=ABCDEFGHIJK");
		}

		[Test]
		public void CanCreateConnectionWithPathAndCustomQueryStringParameters()
		{
			// Arrange
			var uri = new Uri("http://localhost:9000");
			var query = new NameValueCollection { { "authToken", "ABCDEFGHIJK" } };
			var connectionSettings = new ConnectionSettings(uri, "index").SetGlobalQueryStringParameters(query);
			var client = new ElasticClient(connectionSettings, new InMemoryConnection(connectionSettings));
			var result = client.IndexExists(ie=>ie.Index("index"));

			// Assert
			Assert.AreEqual("http://localhost:9000/index?authToken=ABCDEFGHIJK", result.ConnectionStatus.RequestUrl);
		}


		[Test]
		public void SendStringAsJsonBody()
		{
			var jsonAsString = "{ \"json_as_a_string\" : true}";
			var result = this._client.Raw.Bulk(jsonAsString, qs => qs
				.Replication(Replication.Async)
				.Refresh(true)
			);
			StringAssert.EndsWith(":9200/_bulk?replication=async&refresh=true", result.RequestUrl);
			Assert.AreEqual(jsonAsString, result.Request.Utf8String());
		}

		[Test]
		public void SendAnonymousObjectAsJsonBody()
		{
			var jsonAsString = string.Format("{{{0}  \"json_as_a_string\": true{0}}}", System.Environment.NewLine);
			var result = this._client.Raw.Bulk(
				new { json_as_a_string = true }
				, qs => qs
					.Replication(Replication.Async)
					.Refresh(true)
			);
			StringAssert.EndsWith(":9200/_bulk?replication=async&refresh=true", result.RequestUrl);
			Assert.AreEqual(jsonAsString, result.Request.Utf8String());
		}
	}
}