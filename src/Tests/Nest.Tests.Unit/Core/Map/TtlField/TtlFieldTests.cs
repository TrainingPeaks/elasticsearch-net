﻿using NUnit.Framework;
using Nest17.Tests.MockData.Domain;
using System.Reflection;

namespace Nest17.Tests.Unit.Core.Map.TtlField
{
	[TestFixture]
	public class TtlFieldTests : BaseJsonTests
	{
		[Test]
		public void TtlFieldSerializes()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.TtlField(t => t
					.Enable()
					.Default("1d")
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
	}
}
