﻿using System;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Unit.Core.Update
{
	[TestFixture]
	public class UpdateRequestTests : BaseJsonTests
	{

		[Test]
		public void Inferred()
		{
			var result = this._client.Update<ElasticsearchProject>(u => u
				.IdFrom(new ElasticsearchProject { Id = 2 })
			);
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/nest_test_data/elasticsearchprojects/2/_update", uri.AbsolutePath);
			StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
		}
		[Test]
		public void InferredWithOverrides()
		{
			var result = this._client.Update<ElasticsearchProject>(u => u
				.Index("myindex")
				.IdFrom(new ElasticsearchProject { Id = 2 })
			);
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/myindex/elasticsearchprojects/2/_update", uri.AbsolutePath);
			StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
		}
		[Test]
		public void NotEnoughInfoThrows()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var result = this._client.Update<ElasticsearchProject>(u => u
					.Index("myindex")
				);
			});
		}
		[Test]
		public void AllFilledIn()
		{
			var result = this._client.Update<ElasticsearchProject>(u => u
				.Index("myindex")
				.Type("mytype")
				.Id("1")
				);
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/myindex/mytype/1/_update", uri.AbsolutePath);
			StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
		}
		
		[Test]
		public void ScriptQueryStringShouldNotUseGet()
		{
			var result = this._client.Update<ElasticsearchProject>(u => u
				.Index("myindex")
				.Type("mytype")
				.Id("1")
				.ScriptQueryString("_source.something = 1;")
			);
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/myindex/mytype/1/_update", uri.AbsolutePath);
			Assert.AreEqual("?script=_source.something%20%3D%201%3B", uri.Query);
			StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
		}
	}
}