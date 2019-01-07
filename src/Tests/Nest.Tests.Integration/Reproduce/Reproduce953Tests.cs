using System.Text;
using ES.Net.Connection;
using ES.Net.ConnectionPool;
using FluentAssertions;
using Nest17.Tests.Integration;
using Nest17.Tests.MockData;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest17.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce953Tests 
	{
		[Test]
		public void Calling_Refresh_UsingHttpClientConnection_DoesNotThrow()
		{

			var settings = ElasticsearchConfiguration.Settings()
				.EnableCompressedResponses(true);
			var connection = new HttpClientConnection(settings);
			var client = new ElasticClient(settings, connection: connection);
			
			Assert.DoesNotThrow(()=> client.Refresh());
			Assert.DoesNotThrow(()=> client.Get<ElasticsearchProject>(NestTestData.Data.First().Id));
			Assert.DoesNotThrow(()=> client.Ping());

		}
		
		
	}
}
