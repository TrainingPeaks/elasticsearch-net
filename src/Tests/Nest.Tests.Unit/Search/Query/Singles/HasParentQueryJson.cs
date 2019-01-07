﻿using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class HasParentQueryJson
	{
		[Test]
		public void HasParentThisQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.HasParent<Person>(fz => fz
						.Name("named_query")
						.Query(qq=>qq.Term(f=>f.FirstName, "john"))
						.Score(ParentScoreType.Score)
                   )
            );
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ has_parent: { 
				_name: ""named_query"",
				type: ""person"",
				score_type: ""score"",
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
