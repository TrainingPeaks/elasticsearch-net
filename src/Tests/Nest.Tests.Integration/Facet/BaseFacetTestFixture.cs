﻿using System.Linq;
using NUnit.Framework;
using Nest17.Tests.MockData;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Integration.Facet
{
	public class BaseFacetTestFixture : IntegrationTests
	{
		protected string _LookFor = NestTestData.Data.First().Followers.First().LastName;

		protected void TestDefaultAssertions(ISearchResponse<ElasticsearchProject> queryResponse)
		{
			Assert.True(queryResponse.IsValid, "response is not valid");
			Assert.NotNull(queryResponse.ConnectionStatus, "connection status is null");
			Assert.Null(queryResponse.ConnectionStatus.OriginalException, "exception should not be set");
			Assert.True(queryResponse.Total > 0, "Query yielded no results as indicated by total returned from ES");
			Assert.True(queryResponse.Documents.Any(), "documents.any() is false");
			Assert.True(queryResponse.Documents.Count() > 0, "documents.count is 0");
			Assert.True(queryResponse.Shards.Total > 0, "did not hit any shard");
			Assert.True(queryResponse.Shards.Successful == queryResponse.Shards.Total, "Not all the shards were hit succesfully");
			Assert.True(queryResponse.Shards.Failed == 0, "shards failed is not null");
		}
	}
}