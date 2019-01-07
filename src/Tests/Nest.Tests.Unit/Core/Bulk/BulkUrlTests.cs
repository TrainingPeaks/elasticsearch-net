﻿using System;
using ES.Net;
using FluentAssertions;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Core.Bulk
{
	[TestFixture]
	public class BulkUrlTests : BaseJsonTests
	{
		[Test]
		public void BulkNonFixed()
		{
			var result = this._client.Bulk(b => b
				.Index<ElasticsearchProject>(i => i.Document(new ElasticsearchProject {Id = 2}))
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 3 }))
				.Delete<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 4 }))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/_bulk");
		}

		[Test]
		public void BulkNonFixedWithParams()
		{
			var result = this._client.Bulk(b => b
				.Refresh()
				.Consistency(Consistency.Quorum)
				.Index<ElasticsearchProject>(i => i.Document(new ElasticsearchProject {Id = 2}))
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 3 }))
				.Delete<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 4 }))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/_bulk");
			uri.Query.Should().Be("?refresh=true&consistency=quorum");
		}

		[Test]
		public void BulkFixedIndex()
		{
			var result = this._client.Bulk(b => b
				.FixedPath("myindex")
				.Index<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 2 }))
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 3 }))
				.Delete<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 4 }))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/_bulk");
		}
		[Test]
		public void BulkFixedIndexAndType()
		{
			var result = this._client.Bulk(b => b
				.FixedPath("myindex", "mytype")
				.Index<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 2 }))
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 3 }))
				.Delete<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 4 }))
			);
			var status = result.ConnectionStatus;
			var uri = new Uri(result.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.Should().Be("/myindex/mytype/_bulk");
		}
	}
}
