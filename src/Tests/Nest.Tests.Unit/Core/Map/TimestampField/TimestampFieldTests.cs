﻿using NUnit.Framework;
using Nest17.Tests.MockData.Domain;
using System.Reflection;

namespace Nest17.Tests.Unit.Core.Map.TimestampField
{
	[TestFixture]
	public class TimestampFieldTests : BaseJsonTests
	{
		[Test]
		public void TimestampFieldUsingExpression()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.TimestampField(a => a
					.Path(p => p.Name)
					.Enabled(false)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
		[Test]
		public void TimestampFieldUsingString()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.TimestampField(a => a
					.Path("my_difficult_field_name")
					.Enabled()
                    .Store()
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
