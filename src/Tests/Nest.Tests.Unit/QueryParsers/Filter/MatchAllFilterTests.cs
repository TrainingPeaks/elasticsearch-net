using NUnit.Framework;

namespace Nest17.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class MatchAllFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void MatchAll_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var matchAllFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.MatchAll,
				f=>f.MatchAll()
				);
		}
		
	}
}