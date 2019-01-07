﻿using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Unit.Search.Filter.Singles
{
    [TestFixture]
    public class GeoHashCellFilterJson
    {
        [Test]
        public void GeoHashCellFilter()
        {
            var s = new SearchDescriptor<ElasticsearchProject>()
                .From(0)
                .Size(10)
                .Filter(filter => filter
                    .Cache(true)
                    .Name("my_geo_hash_cell_filter")
                    .GeoHashCell(f => f.Origin, d => d
                        .Location(lat: 13.4080, lon: 52.5186)
                        .Precision(3)
                        .Neighbors(true))
                );

            var json = TestElasticClient.Serialize(s);
            var expected = @"{ from: 0, size: 10, 
                filter : {
                            geohash_cell: {
                                origin: ""{ lat: 13.408, lon: 52.5186 }"",
                                precision: 3,
                                neighbors: true,
                                _cache: true,
                                _name: ""my_geo_hash_cell_filter""
                            }
                        }
            }";
            Assert.True(json.JsonEquals(expected), json);
        }
    }

}
