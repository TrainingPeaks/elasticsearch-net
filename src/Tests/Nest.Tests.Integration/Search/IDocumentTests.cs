using System.Linq;
using Nest17.Tests.MockData;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace Nest17.Tests.Integration.Search
{
	[TestFixture]
	public class IDocumentTests : IntegrationTests
	{
		[Test]
		public void Search()
		{
			var searchResults = this.Client.Search<IDocument>(c=>c
				.Index(ElasticsearchConfiguration.DefaultIndexPrefix + "*")
				.AllTypes()
			);
			searchResults.Total.Should().BeGreaterThan(0);
			var project = searchResults.Documents.First().As<ElasticsearchProject>();
			project.Should().NotBeNull();
			project.Name.Should().NotBeNullOrEmpty();
		}

	}
}