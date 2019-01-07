﻿using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;
using ES.Net;

namespace Nest17.Tests.Integration.Core.Suggest
{
	[TestFixture]
	public class SuggestTests : IntegrationTests
	{
		[Test]
		public void TestSuggest()
		{
			var country = this.Client.Search<ElasticsearchProject>(s => s.Size(1)).Documents.First().Country;
			var wrongCountry = country + "x";

			var suggestResults = Client.Suggest<ElasticsearchProject>(s => s
				.Term("mySuggest", m => m
					.SuggestMode(SuggestMode.Always)
					.Text(wrongCountry)
					.Size(1)
					.OnField("country")
				)
			);

			suggestResults.IsValid.Should().BeTrue();

			suggestResults.Shards.Should().NotBeNull();
			suggestResults.Suggestions.Should().NotBeNull().And.HaveCount(1);
			var suggestions = suggestResults.Suggestions["mySuggest"];
			suggestions.Should().NotBeNull().And.NotBeEmpty();

			var suggestion = suggestions.First();
			suggestion.Text.Should().Be(wrongCountry);
			var option = suggestion.Options.First();
			option.Text.Should().Be(country);

		}

		
	}
}
