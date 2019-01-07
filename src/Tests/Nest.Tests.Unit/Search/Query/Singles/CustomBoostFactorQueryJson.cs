﻿using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class CustomBoostFactorQueryJson
	{
		[Test]
		public void CustomBoostFactorQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(q=>q
					//disabling obsolete message in this test
					#pragma warning disable 0618
						.CustomBoostFactor(cs=>cs
							.Name("named_query")
							.BoostFactor(5.2)
							.Query(qq=>qq.MatchAll())
					)
					#pragma warning restore 0618
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						custom_boost_factor : { 
							_name: ""named_query"",
							query : { match_all : {} },
							boost_factor : 5.2
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
