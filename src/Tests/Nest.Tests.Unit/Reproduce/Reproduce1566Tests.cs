using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ES.Net;
using FluentAssertions;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce1566Tests : BaseJsonTests
	{
		[Test]
		public void RawSurvives()
		{
			var searchRequest = new SearchRequest()
			{
				Aggregations = new Dictionary<string, IAggregationContainer>(),
			};

			var filterContainer = new AggregationContainer()
			{
				Filter = new FilterAggregator
				{
					Filter = new FilterDescriptor<dynamic>().Raw(@"{""test"":""test""")
				},
				Aggregations = new Dictionary<string, IAggregationContainer>()
			};

			searchRequest.Aggregations["base_filter"] = filterContainer;
			var serialized = _client.Serializer.Serialize(searchRequest).Utf8String();
			serialized.Should().Contain(@"""test""");

		}
	}
}
