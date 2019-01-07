﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Integration.Debug
{
	[TestFixture]
	public class MemoryUsageTests 
	{
		[Test]
		public void DeserializeOfStreamDoesNotHoldACopyOfTheResponse()
		{
			var uri = ElasticsearchConfiguration.CreateBaseUri();
			var settings = new ConnectionSettings(uri, ElasticsearchConfiguration.DefaultIndex);
			IElasticClient client = new ElasticClient(settings);
			
			var results = client.Search<ElasticsearchProject>(s => s.MatchAll());


		}

	}
}
