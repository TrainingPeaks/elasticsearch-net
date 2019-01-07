﻿using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class NestedQueryJson
	{
		[Test]
		public void NestedQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff=>ff
					.Nested(n=>n
						.Name("named_query")
						.Path(f=>f.Followers[0])
						.Query(q=>q.Term(f=>f.Followers[0].FirstName,"elasticsearch.pm"))
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
					nested: {
						_name: ""named_query"",
						query: {
							term: {
								""followers.firstName"": { value: ""elasticsearch.pm"" }
							}
						},
						path: ""followers""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
