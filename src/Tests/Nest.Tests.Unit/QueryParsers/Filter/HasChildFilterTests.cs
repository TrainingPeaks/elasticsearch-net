using FluentAssertions;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class HasChildFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void HasChild_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var hasChildFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.HasChild,
				f=>f.HasChild<Person>(d=>d
					.Query(q => q.Term(p => p.FirstName, "value"))
					.Filter(q => q.Term(p => p.Age, 42))
					.InnerHits()
				)
			);

			var query = hasChildFilter.Query;
			hasChildFilter.InnerHits.Should().NotBeNull();

			query.Should().NotBeNull();
			query.Term.Field.Should().Be("firstName");

			var filter = hasChildFilter.Filter;
			filter.Should().NotBeNull();
			filter.Term.Field.Should().Be("age");
		}
		
	}
}