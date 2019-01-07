﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Integration.Index
{
	[TestFixture]
	public class IndexUsingUrlIdTests : IntegrationTests
	{
		[Test]
		public void IndexUsingAnUrlAsId()
		{
			var id = "http://www.skusuplier.com/products/123?affiliateId=23131#oopsIcopiedAnAnchor";
			var newProduct = new Product
			{
				Id = id,
				Name = "Top Product"
			};
			var response = this.Client.Index(newProduct);

			var productInElasticsearch = this.Client.Source<Product>(i=>i.Id(id));
			Assert.NotNull(productInElasticsearch);
			Assert.AreEqual(productInElasticsearch.Id, id);
			Assert.True(response.IsValid);
		}


		[Test]
		public void IndexUsingAnUrlAsIdUsingCustomUrlParameterInSettings()
		{
			var settings = ElasticsearchConfiguration.Settings().SetGlobalQueryStringParameters(new NameValueCollection
			{
				{"apiKey", "my-api-key"}
			});
			var client = new ElasticClient(settings);

			var id = "http://www.skusuplier.com/products/123?affiliateId=23131#oopsIcopiedAnAnchor";
			var newProduct = new Product
			{
				Id = id,
				Name = "Top Product"
			};

			var response = client.Index(newProduct);

			var productInElasticsearch = client.Source<Product>(id);
			Assert.NotNull(productInElasticsearch);
			Assert.AreEqual(productInElasticsearch.Id, id);
			Assert.True(response.IsValid);
		}
	}
}
