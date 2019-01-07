﻿using System.Linq;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;
using ES.Net;

namespace Nest17.Tests.Integration.Search
{
	[TestFixture]
	public class HighlightIntegrationTests : IntegrationTests
	{
		[Test]
		public void TestHighlight()
		{
			var result = this.Client.Search<ElasticsearchProject>(s => s
			  .From(0)
			  .Size(10)
			  .Query(q => q
				.QueryString(qs => qs
				  .DefaultField(e => e.Content)
				  .Query("null or null*")
				)
			  )
			  .Highlight(h => h
				.PreTags("<b>")
				.PostTags("</b>")
				.OnFields(
				  f => f
					.OnField(e => e.Content)
					.PreTags("<em>")
					.PostTags("</em>")
					
				)
			  )
			);
			Assert.IsTrue(result.IsValid);
			Assert.DoesNotThrow(() => result.Highlights.Count());
			Assert.IsNotNull(result.Highlights);
			Assert.Greater(result.Highlights.Count(), 0);
			Assert.True(result.Highlights.All(h => h.Value != null));

			Assert.True(result.Highlights.All(h => h.Value.All(hh => !hh.Value.DocumentId.IsNullOrEmpty())));

			var id = result.Documents.First().Id.ToString();
			var highlights = result.Highlights[id];	
			Assert.NotNull(highlights);
			Assert.NotNull(highlights["content"]);
			Assert.Greater(highlights["content"].Highlights.Count(), 0);

			highlights = result.Hits.First().Highlights;
			Assert.NotNull(highlights);
			Assert.NotNull(highlights["content"]);
			Assert.Greater(highlights["content"].Highlights.Count(), 0);
		}

		[Test]
		public void TestHighlightNoNullRef()
		{
			var result = this.Client.Search<ElasticsearchProject>(s => s
			  .From(0)
			  .Size(10)
			  .Query(q => q
				.QueryString(qs => qs
				  .Query("elasticsearch.pm")
				)
			  )
			  .Highlight(h => h
				.PreTags("<b>")
				.PostTags("</b>")
				.OnFields(
				  f => f
					.OnField(e => e.Content)
					.PreTags("<em>")
					.PostTags("</em>")
				)
			  )
			);
			Assert.IsTrue(result.IsValid);
			Assert.DoesNotThrow(() => result.Highlights.Count());
			Assert.IsNotNull(result.Highlights);
			Assert.GreaterOrEqual(result.Total, 1);
			Assert.AreEqual(result.Highlights.Count(), 0);
		}

		[Test]
		public void TestHighlightQuery()
		{
			var result = this.Client.Search<ElasticsearchProject>(s => s
			  .From(0)
			  .Size(10)
			  .Query(q => q
				.QueryString(qs => qs
				  .Query("elasticsearch.pm")
				)
			  )
			  .Highlight(h => h
				.HighlightQuery(hq => hq
					.Bool(b => b
						.Must(m => m
							.Match(mm => mm
								.OnField(p => p.Name)
								.Query("elasticsearch.pm")
							)
						)
						.Should(sh => sh
							.MatchPhrase(mp => mp
								.OnField(p => p.Name)
								.Slop(1)
								.Boost(10)
							)
						)
						.MinimumShouldMatch(0)
					)
				)
				.PreTags("<b>")
				.PostTags("</b>")
				.OnFields(
				  f => f
					.OnField(e => e.Name)
					.PreTags("<em>")
					.PostTags("</em>")
				)
			  )
			);

			Assert.IsTrue(result.IsValid);
			Assert.DoesNotThrow(() => result.Highlights.Count());
			Assert.IsNotNull(result.Highlights);
			Assert.GreaterOrEqual(result.Total, 2);
			Assert.AreEqual(result.Highlights.Count(), 2);
		}
	}
}
