﻿using System;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework.Constraints;

namespace Nest17.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class IndicesFilterJson
	{
		[Test]
		public void IndicesFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter=>filter
					.Indices(i=>i
						.Indices(new [] { "index1", "index2"})
						.Filter(f=>f.Term(p=>p.Name, "NEST"))
						.NoMatchFilter(f=>f.Term(p=>p.Name, "Elasticsearch.NET"))
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{
  from: 0,
  size: 10,
  filter: {
    indices: {
      indices: [
        ""index1"",
        ""index2""
      ],
      filter: {
        term: {
          name: ""NEST""
        }
      },
      no_match_filter: {
        term: {
          name: ""Elasticsearch.NET""
        }
      }
    }
  }
}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void IndicesFilterWithShortcut()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter=>filter
					.Indices(i=>i
						.Indices(new [] { "index1", "index2"})
						.Filter(f=>f.Term(p=>p.Name, "NEST"))
						.NoMatchFilter(NoMatchShortcut.None)
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{
  from: 0,
  size: 10,
  filter: {
    indices: {
      indices: [
        ""index1"",
        ""index2""
      ],
      filter: {
        term: {
          name: ""NEST""
        }
      },
      no_match_filter: ""none""
    }
  }
}";
			Assert.True(json.JsonEquals(expected), json);
		}
		
	}
}
