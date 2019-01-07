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
	public class Reproduce1169Tests : IntegrationTests
	{
		[Test]
		[SkipVersion("0 - 1.2.9", "Top hits agg added in 1.3")]
		public void TopHitsReturnedFirst()
		{
			var response = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.TopHits("my-top-hits", th => th
						.Size(1)
					)
					.Terms("a", t => t
						.Field(p => p.Name)
					)
				)
			);

			response.IsValid.Should().BeTrue();
			
			var topHits = response.Aggs.TopHitsMetric("my-top-hits");
			topHits.Should().NotBeNull();
			topHits.Hits<ElasticsearchProject>().Count().Should().BeGreaterThan(0);

			var terms = response.Aggs.Terms("a");
			terms.Should().NotBeNull();
			terms.Items.Count().Should().BeGreaterThan(0);
		}

		[Test]
		[SkipVersion("0 - 1.2.9", "Top hits agg added in 1.3")]
		public void TermsReturnedFirst()
		{
			var response = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.TopHits("my-top-hits", th => th
						.Size(1)
					)
					.Terms("b", t => t
						.Field(p => p.Name)
					)
				)
			);

			response.IsValid.Should().BeTrue();

			var topHits = response.Aggs.TopHitsMetric("my-top-hits");
			topHits.Should().NotBeNull();
			topHits.Hits<ElasticsearchProject>().Count().Should().BeGreaterThan(0);

			var terms = response.Aggs.Terms("b");
			terms.Should().NotBeNull();
			terms.Items.Count().Should().BeGreaterThan(0);
		}
	}
}
