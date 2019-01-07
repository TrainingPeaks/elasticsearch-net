﻿using System.Reflection;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Unit.Search.Query.Singles.CommonTerms
{
	[TestFixture]
	public class CommonTermsTests : BaseJsonTests
	{
		[Test]
		public void CommonTermsFull()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.CommonTerms(qs=>qs
						.Name("named_query")
						.OnField(p=>p.Content)
						.Analyzer("myAnalyzer")
						.Boost(1.2)
						.CutoffFrequency(0.01)
						.DisableCoord(false)
						.HighFrequencyOperator(Operator.And)
						.LowFrequencyOperator(Operator.Or)
						.MinimumShouldMatch(1)
						.Query("This is the most awful stopwords query ever")
					)
			);
				
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}
