﻿using System.Linq;
using NUnit.Framework;

namespace Nest17.Tests.Integration.Search.Query
{
	[TestFixture]
	public class TermQueryDynamic : IntegrationTests
	{
		[Test]
		public void TestTermQuery()
		{
			var results = this.Client.Search<dynamic>(s=>s
				.Index(ElasticsearchConfiguration.DefaultIndex)
				.Type("elasticsearchprojects")
				.From(0)
				.Size(10)
				.Query(q => q
					.Term("name", "elasticsearch.pm")
				)
			);

			Assert.True(results.IsValid);
			Assert.Greater(results.Documents.Count(), 0);
			var first = results.Documents.First();
			Assert.IsNotNullOrEmpty((string)first.followers[0].firstName);
		}
	}
}
