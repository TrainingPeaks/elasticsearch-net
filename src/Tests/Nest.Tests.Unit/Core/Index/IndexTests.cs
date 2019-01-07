﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Core.Index
{
	[TestFixture]
	public class IndexTests : BaseJsonTests
	{
		[Test]
		public void IndexParameters()
		{
			var o = new ElasticsearchProject { Id = 1, Name = "Test" };
			var result = this._client.Index(o, i => i.Version(1));
			var status = result.ConnectionStatus;
			StringAssert.Contains("version=1", status.RequestUrl);
		}

		[Test]
		public void IndexingDictionaryRespectsCasing()
		{
			var x = new
			{
				FirstDictionary = new Dictionary<string, object>
				{
					{"ALLCAPS", 1 },
					{"PascalCase", "should work as well"},
					{"camelCase", DateTime.Now}
				}
			};
			var result = this._client.Index(x, i=>i.Id(1));

			var request = result.ConnectionStatus.Request.Utf8String();
			StringAssert.Contains("ALLCAPS", request);
			StringAssert.Contains("PascalCase", request);
			StringAssert.Contains("camelCase", request);
			StringAssert.Contains("firstDictionary", request);
		}
	}
}
