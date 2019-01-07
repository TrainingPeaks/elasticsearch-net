﻿using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Search.Facets
{
  [TestFixture]
  public class FilterFacetJson
  {
    [Test]
    public void FilterFacet()
    {
      var s = new SearchDescriptor<ElasticsearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetFilter("wow_facet", filter=>filter
          .Exists(f=>f.Name)
        );

      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""wow_facet"" :  {
                filter : {
                  exists : { field : ""name"" }
               } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
   
  }
}
