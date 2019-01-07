﻿using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class TopChildrenQueryJson
	{
		[Test]
		public void TopChildrenQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.TopChildren<Person>(fz => fz
						.Name("named_query")
						.Query(qq=>qq.Term(f=>f.FirstName, "john"))
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ top_children: { 
				type: ""person"",
				_name: ""named_query"",
				query: {
					term: {
						firstName: {
							value: ""john""
						}
					}
				}
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void HasChildOverrideTypeQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.TopChildren<Person>(fz => fz
						.Query(qq => qq.Term(f => f.FirstName, "john"))
						.Score(TopChildrenScore.Average)
						.Type("sillypeople")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ top_children: { 
				type: ""sillypeople"",
				score: ""avg"",
				query: {
					term: {
						firstName: {
							value: ""john""
						}
					}
				}

			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
