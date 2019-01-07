﻿using System;
using System.Collections.Generic;
using Nest17;

namespace Profiling.Indexing
{
	public class HttpTester : Tester
	{
		public override IElasticClient CreateClient(string indexName)
		{
			var settings = this.CreateSettings(indexName, 9200);
			var client = new ElasticClient(settings);
			return client;
		}
		

	}
}
