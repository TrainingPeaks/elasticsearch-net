﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Core.Template
{
	[TestFixture]
	public class PutWarmerRequestTests : BaseJsonTests
	{

		[Test]
		public void PathWithType()
		{
			var result = this._client.PutWarmer("warmer_pathwithtype", wd => wd
				.Index<ElasticsearchProject>()
				.Type<ElasticsearchProject>()
				.Search<ElasticsearchProject>(s => s));
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			StringAssert.Contains("using Nest17 IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
			StringAssert.EndsWith("/nest_test_data/elasticsearchprojects/_warmer/warmer_pathwithtype", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}

		[Test]
		public void PathWithDynamic()
		{
			var result = this._client.PutWarmer("warmer_pathwithdynamic", wd=> wd
				.Index("my_index")
				.Search<dynamic>(s => s));
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			StringAssert.Contains("using Nest17 IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
			StringAssert.EndsWith("/my_index/_warmer/warmer_pathwithdynamic", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}

		[Test]
		public void PathWithAllIndices()
		{
			var result = this._client.PutWarmer("warmer_pathwithallindices", wd => wd
				.AllIndices()
				.Search<dynamic>(s => s));
			Assert.NotNull(result, "PutWarmer result should not be null");
			var status = result.ConnectionStatus;
			StringAssert.Contains("using Nest17 IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
			StringAssert.EndsWith("/_all/_warmer/warmer_pathwithallindices", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}
	}
}
