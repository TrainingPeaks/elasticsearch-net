﻿using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class CustomScoreQueryJson
	{
		[Test]
		public void CustomScoreQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q=>q
					//disabling obsolete message in this test
					#pragma warning disable 0618
					.CustomScore(cs=>cs
						.Name("named_query")
						.Script("doc['num1'].value > 1")
						.Query(qq=>qq.MatchAll())
					)
					#pragma warning restore 0618
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						custom_score : { 
							_name: ""named_query"",
							script : ""doc['num1'].value > 1"",
							query : { match_all : {} }
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void CustomScoreQueryParams()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q => q
					//disabling obsolete message in this test
					#pragma warning disable 0618
					.CustomScore(cs => cs
						.Script("doc['num1'].value > myvar")
						.Params(p=>p.Add("myvar", 1.0))
						.Query(qq => qq.MatchAll())
					)
					#pragma warning restore 0618
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						custom_score : { 
							script : ""doc['num1'].value > myvar"",
							params : { myvar : 1.0 },
							query : { match_all : {} }
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
