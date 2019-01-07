using System;
using System.Collections.Generic;
using ES.Net.Connection;
using Nest17;

namespace Profiling.Indexing
{
	public class HttpClientTester : Tester
	{
		public override IElasticClient CreateClient(string indexName)
		{
			var settings = this.CreateSettings(indexName, 9200);
			var client = new ElasticClient(settings, new HttpClientConnection(settings));
			return client;
		}
		

	}
}
