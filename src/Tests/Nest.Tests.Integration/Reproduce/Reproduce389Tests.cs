﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Integration.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce389Tests : IntegrationTests
	{

		/// <summary>
		///	https://github.com/Mpdreamz/NEST/issues/308
		/// </summary>
		[Test]
		public void DoesPreferenceOnTheBodyThrowAnElasticsearchException()
		{
			var result = Client.Search<ElasticsearchProject>(op => op
				.Query(q => 
					q.Term(t => t.Name, "nest")
				)
				.ExecuteOnPrimary()
				.Fields(
					field => field.Name
				)
				.From(0)
				.Size(2)
			);
			Assert.True(result.IsValid, result.ConnectionStatus.ToString());
			StringAssert.Contains("preference=_primary", result.ConnectionStatus.RequestUrl);




		}

	}
}
