﻿using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Search.Scroll.Scroll
{
	public class ScrollApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, IScrollRequest, ScrollDescriptor<Project>, ScrollRequest>
	{
		private string _scrollId = "default-for-unit-tests";

		public ScrollApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			scroll = "1m",
			scroll_id = _scrollId
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<ScrollDescriptor<Project>, IScrollRequest> Fluent => s => s.Scroll("1m").ScrollId(_scrollId);
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ScrollRequest Initializer => new ScrollRequest(_scrollId, "1m");
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_search/scroll";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Scroll("1m", _scrollId, f),
			(c, f) => c.ScrollAsync("1m", _scrollId, f),
			(c, r) => c.Scroll<Project>(r),
			(c, r) => c.ScrollAsync<Project>(r)
		);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var response = client.Search<Project>(s => s.MatchAll().Scroll(TimeSpan.FromMinutes(1)));
			if (!response.IsValid)
				throw new Exception("Scroll setup failed");

			_scrollId = response.ScrollId ?? _scrollId;
		}

		protected override void OnAfterCall(IElasticClient client) => client.ClearScroll(cs => cs.ScrollId(_scrollId));
	}
}
