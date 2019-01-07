﻿using FluentAssertions;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest17.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1181Tests : IntegrationTests
	{
		[Test]
		[SkipVersion("0 - 1.3.9", "Filters aggregation added in ES 1.4")]
		public void FiltersAggregationResponseNotReadToCompletion()
		{
			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.Terms("name-count", t => t
						.Field(p => p.Name)
					)
					.Terms("country-count", t => t
						.Field(p => p.Country)
					)
					.Filters("my-filters", fs => fs
						.Filters(
							f => f.Name("country-filter").Query(q => q.QueryString(qs => qs.Query("Sweden").OnFields(p => p.Country))),
							f => f.Name("name-filter").Query(q => q.QueryString(qs => qs.Query("elasticsearch").OnFields(p => p.Name)))
						)
					)
				)
			);

			result.IsValid.Should().BeTrue();
			result.Aggregations.Count.Should().Be(3);
		}
	}
}
