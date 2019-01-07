﻿using System;
using System.Linq;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;
using System.Reflection;

namespace Nest17.Tests.Unit.Core.Map.SourceField
{
	[TestFixture]
	public class SourceFieldTests : BaseJsonTests
	{
		[Test]
		public void SourceFieldSerializesFully()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.SourceField(s => s
					.Enabled(false)
					.Compress()
					.CompressionThreshold("200b")
					.Excludes(new[] { "path1.*" })
					.Includes(new[] { "path2.*" })
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
	}
}
