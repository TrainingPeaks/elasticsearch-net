using System.Linq;
using Nest17.Tests.MockData;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;
using ES.Net;

namespace Nest17.Tests.Integration.Search
{
	[TestFixture]
	public class VersionTests : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void WithVersion()
		{
			var queryResults = this.Client.Search<ElasticsearchProject>(s=>s
				.Version()
				.MatchAll() //not explicitly needed.
			);
			Assert.True(queryResults.IsValid);
			Assert.Greater(queryResults.Total, 0);
			Assert.True(queryResults.Hits.All(h => !h.Version.IsNullOrEmpty()));
		}
		[Test]
		public void NoVersion()
		{
			var queryResults = this.Client.Search<ElasticsearchProject>(s => s
				   .Version(false)
				   .MatchAll() //not explicitly needed.
			   );

			Assert.True(queryResults.IsValid);
			Assert.Greater(queryResults.Total, 0);
			Assert.True(queryResults.Hits.All(h => h.Version.IsNullOrEmpty()));
		}
	}
}