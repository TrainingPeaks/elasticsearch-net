﻿using System;
using FluentAssertions;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Integration.Mapping
{
	[TestFixture]
	public class MapTests : IntegrationTests
	{
		private void TestMapping(RootObjectMapping typeMapping)
		{
			Assert.NotNull(typeMapping);
			Assert.AreEqual("string", typeMapping.Properties["content"].Type.Name);
			Assert.AreEqual("string", typeMapping.Properties["country"].Type.Name);
			Assert.AreEqual("double", typeMapping.Properties["doubleValue"].Type.Name);
			Assert.AreEqual("long", typeMapping.Properties["longValue"].Type.Name);
			Assert.AreEqual("boolean", typeMapping.Properties["boolValue"].Type.Name);
			Assert.AreEqual("integer", typeMapping.Properties["intValues"].Type.Name);
			Assert.AreEqual("float", typeMapping.Properties["floatValues"].Type.Name);
			Assert.AreEqual("string", typeMapping.Properties["name"].Type.Name);
			Assert.AreEqual("date", typeMapping.Properties["startedOn"].Type.Name);
			Assert.AreEqual("long", typeMapping.Properties["stupidIntIWantAsLong"].Type.Name);
			Assert.AreEqual("float", typeMapping.Properties["floatValue"].Type.Name);
			Assert.AreEqual("integer", typeMapping.Properties["id"].Type.Name);
			Assert.AreEqual("integer", typeMapping.Properties["loc"].Type.Name);
			Assert.AreEqual("geo_point", typeMapping.Properties["origin"].Type.Name);
			Assert.AreEqual("object", typeMapping.Properties["product"].Type.Name);

			var productMapping = typeMapping.Properties["product"] as ObjectMapping;
			Assert.NotNull(productMapping);
			Assert.AreEqual("string", productMapping.Properties["name"].Type.Name);
			Assert.AreEqual("string", productMapping.Properties["id"].Type.Name);

			var countryMapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(countryMapping);
			Assert.AreEqual(FieldIndexOption.NotAnalyzed, countryMapping.Index);
		}

		[Test]
		public void SimpleMapByAttributes()
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var x = this.Client.CreateIndex(index, s => s
				.AddMapping<ElasticsearchProject>(m => m.MapFromAttributes())
			);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());

			var typeMapping = this.Client.GetMapping<ElasticsearchProject>(i => i.Index(index).Type("elasticsearchprojects"));
			typeMapping.Should().NotBeNull();
			TestMapping(typeMapping.Mapping);
		}


		[Test]
		public void SimpleMapByAttributesUsingType()
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var x = this.Client.CreateIndex(index, s => s
				.AddMapping<ElasticsearchProject>(a=>a.MapFromAttributes())
			);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());
			var xx = this.Client.Map<object>(m=>m.Type(typeof(ElasticsearchProject)).Index(index));
			Assert.IsTrue(xx.Acknowledged);

			var typeMapping = this.Client.GetMapping<ElasticsearchProject>(i => i.Index(index).Type("elasticsearchprojects"));
			typeMapping.Should().NotBeNull();
			TestMapping(typeMapping.Mapping);
		}

		[Test]
		public void GetMapping()
		{
			var typeMapping = this.Client.GetMapping<ElasticsearchProject>(i => i.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects"));
			typeMapping.Should().NotBeNull();
			TestMapping(typeMapping.Mapping);
		}

		[Test]
		public void GetMappingOnNonExistingIndexType()
		{
			Assert.DoesNotThrow(() =>
			{
				var typeMapping = this.Client.GetMapping<ElasticsearchProject>(i=>i.Index("asfasfasfasfasf").Type("asdasdasdasdasdasdasdasd"));
				typeMapping.IsValid.Should().BeFalse();
				Assert.Null(typeMapping.Mapping);
			});

		}

		[Test]
		public void DynamicMap()
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var x = this.Client.CreateIndex(index, s => s);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());
			var typeMappingResult = this.Client.GetMapping<ElasticsearchProject>();
			typeMappingResult.IsValid.Should().BeTrue();
			var typeMapping = typeMappingResult.Mapping;
			var mapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(mapping);
			mapping.Boost = 3;
			this.Client.Map<object>(m=>m.InitializeUsing(typeMapping).Index(index).Type("elasticsearchprojects2"));

			typeMapping = this.Client.GetMapping<ElasticsearchProject>(gm=>gm.Index(index).Type("elasticsearchprojects2")).Mapping;
			var countryMapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(countryMapping);
			Assert.AreEqual(3, countryMapping.Boost);
		}

		[Test]
		public void GetMissingMap()
		{
			Assert.DoesNotThrow(() => {
				var typeMapping = this.Client.GetMapping<ElasticsearchProject>(gm => gm.Index("asdasdasdsada").Type("elasticsearchprojects2")).Mapping;
			});
		}
		
		[Test]
		public void GetAndUpdateMultiFieldMap()
		{
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();

			var indexCreateResponse = this.Client.CreateIndex(indexName);
			Assert.IsTrue(indexCreateResponse.Acknowledged, indexCreateResponse.ConnectionStatus.ToString());

			var mapResponse = this.Client.Map<ElasticsearchProject>(m => m
				.Index(indexName)
				.Properties(props => props
					.MultiField(s => s
						.Name(p => p.Name)
						.Fields(pprops => pprops
							.String(ps => ps.Name(p => p.Name).Index(FieldIndexOption.NotAnalyzed))
							.String(ps => ps.Name(p => p.Name.Suffix("searchable")).Index(FieldIndexOption.Analyzed))
						)
					)
				)
				);
			mapResponse.Should().NotBeNull();
			Assert.IsTrue(mapResponse.IsValid, mapResponse.ConnectionStatus.ToString());

			var getMapResponse = this.Client.GetMapping<ElasticsearchProject>(m => m.Index(indexName));
			getMapResponse.Should().NotBeNull();
			Assert.IsTrue(getMapResponse.IsValid, getMapResponse.ConnectionStatus.ToString());
			getMapResponse.Mapping.Should().NotBeNull();

			mapResponse = this.Client.Map<ElasticsearchProject>(p => p
				.Index(indexName)
				.InitializeUsing(getMapResponse.Mapping)
				);
			mapResponse.Should().NotBeNull();
			Assert.IsTrue(mapResponse.IsValid, mapResponse.ConnectionStatus.ToString());

			var indexDeleteResponse = this.Client.DeleteIndex(x => x.Index(indexName));
			Assert.IsTrue(indexDeleteResponse.Acknowledged, indexDeleteResponse.ConnectionStatus.ToString());
		}
	}
}
