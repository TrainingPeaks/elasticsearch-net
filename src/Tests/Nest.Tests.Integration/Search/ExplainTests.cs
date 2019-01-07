using System.Linq;
using Nest17.Tests.MockData;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;
using ES.Net;

namespace Nest17.Tests.Integration.Search
{
	[TestFixture]
	public class ExplainTests : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void SimpleExplain()
		{
			var queryResults = this.SearchRaw<ElasticsearchProject>(
					@" {
						""explain"": true,
						""query"" : {
							""match_all"" : { }
					} }"
				);
			Assert.True(queryResults.Hits.All(h=>h.Explanation != null));
			Assert.True(queryResults.Hits.All(h => h.Explanation.Details.Any()));
			Assert.True(queryResults.Hits.All(h => h.Explanation.Details.All(d=>!d.Description.IsNullOrEmpty())));
		}
		[Test]
		public void ComplexExplain()
		{
			var queryResults = this.SearchRaw<ElasticsearchProject>(
					@" { ""explain"": true, 
						""query"" : {
						  ""fuzzy"" : { 
							""followers.firstName"" : {
								""value"" : """ + this._LookFor.ToLowerInvariant() + @"x"",
								""boost"" : 1.0,
								""min_similarity"" : 0.5,
								""prefix_length"" : 0
							}
						}
					} }"
				);

			Assert.True(queryResults.Hits.All(h=>h.Explanation != null));
			Assert.True(queryResults.Hits.All(h => h.Explanation.Details.Any()));
			Assert.True(queryResults.Hits.All(h => h.Explanation.Details.All(d=>!d.Description.IsNullOrEmpty())));
		}

		
	}
}
;