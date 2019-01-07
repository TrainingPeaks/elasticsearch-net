﻿using FluentAssertions;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Integration.Core.MoreLikeThis
{
	[TestFixture]
	public class MltSearchBodyTests : IntegrationTests
	{
		[Test]
		public void SearchBodyEndsUpInPost()
		{
			var result = this.Client.MoreLikeThis<ElasticsearchProject>(mlt => mlt
				.Id(1)
				.MltFields(p => p.Country, p => p.Content)
				.MinDocFreq(1)
				.Search(s=>s
					.From(0)
					.Take(20)
				)
			);
			
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Total.Should().BeGreaterOrEqualTo(10);
			result.Documents.Should().NotBeEmpty();
		}
	}
}
