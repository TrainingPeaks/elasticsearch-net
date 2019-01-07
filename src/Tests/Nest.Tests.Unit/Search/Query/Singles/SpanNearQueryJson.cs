﻿using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class SpanNearQueryJson
	{
		[Test]
		public void SpanNearQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.SpanNear(sn => sn
						.Name("named_query")
						.Clauses(
							c => c.SpanTerm(f => f.Name, "elasticsearch.pm", 1.1),
							c => c.SpanFirst(sf => sf
								.MatchTerm(f => f.Name, "elasticsearch.pm", 1.1)
								.End(3)
							)
						)
						.Slop(3)
						.CollectPayloads(false)
						.InOrder(false)
                        .Boost(20.2)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{
				span_near: { 
					_name: ""named_query"",
					clauses: 
					[{
						span_term: { 
								name: {
									value: ""elasticsearch.pm"",
									boost: 1.1
								}
							}
						},
						{ 
							span_first: {
								match: {
									span_term: {
										name: {
											value: ""elasticsearch.pm"",
											boost: 1.1
										}
									}
								},
								end: 3
							}
						}
					],
					slop: 3,
					in_order: false,
					collect_payloads: false,
                    boost: 20.2
			}}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
