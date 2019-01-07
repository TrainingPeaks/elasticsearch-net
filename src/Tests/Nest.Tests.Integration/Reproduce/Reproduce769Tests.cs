﻿using System.Text;
using Nest17.Tests.MockData;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest17.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce769Tests : IntegrationTests
	{
		[Test]
		public void SearchWithTypesAndIndicesSetShouldNotThrow_WithNoDefaultIndex()
		{
			Assert.DoesNotThrow(()=>
			{
				var client = new ElasticClient();
				client.Search<dynamic>(new SearchRequest
				{
					Indices = new IndexNameMarker[] { "index" },
					Types = new TypeNameMarker[] { "type" }
				});
			});
		}
		
		[Test]
		public void EmptySearch_NoDefaultIndex_DoesNotThrow()
		{
			Assert.DoesNotThrow(()=>
			{
				var client = new ElasticClient();
				client.Search<dynamic>(new SearchRequest
				{
				});
			});
		}
	}
}
