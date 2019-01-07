﻿using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class GeoBoundingBoxFilterJson
	{
		[Test]
		public void GeoBoundingBoxFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter => filter
					.GeoBoundingBox(f => f.Origin, 40.73, -74.1, 40.717, -73.99)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					 geo_bounding_box: {
							origin : {
								top_left: ""-74.1, 40.73"",
								bottom_right: ""-73.99, 40.717""
							}
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void GeoBoundingBoxFilterCacheNamed()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter => filter
					.Cache(true)
					.Name("my_geo_filter")
					.GeoBoundingBox(f => f.Origin, 
						topLeftX: 40.73, 
						topLeftY: -74.1, 
						bottomRightX: 40.717, 
						bottomRightY: -73.99, 
						type: GeoExecution.Indexed
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					 geo_bounding_box: {
							origin : {
								top_left: ""-74.1, 40.73"",
								bottom_right: ""-73.99, 40.717""
							},
							type: ""indexed"",
							_cache : true,
							_name : ""my_geo_filter""
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
