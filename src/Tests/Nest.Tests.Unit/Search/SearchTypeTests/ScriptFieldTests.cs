using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Unit.Search.SearchTypeTests
{
	[TestFixture]
	public class SearchTypeTests : BaseJsonTests
	{
		[Test]
		public void SearchTypeDoesNotPolluteQueryObject()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.SearchType(SearchType.QueryAndFetch);
			this.JsonEquals(s, System.Reflection.MethodBase.GetCurrentMethod());
		}
		
	}
}
