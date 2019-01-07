﻿using FluentAssertions;
using Nest17.Tests.MockData.Domain;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest17.Tests.Integration.Search.Template
{
	[TestFixture]
	public class SearchTemplateTests : IntegrationTests
	{
		private string _template = "{\"from\": \"{{my_from}}\",\"size\": \"{{my_size}}\",\"query\": { \"match\": {\"{{my_field}}\": {\"query\": \"{{my_value}}\" }}}}";

		[Test]
		[SkipVersion("0 - 1.0.9", "Search template API added in 1.1")]
		public void SearchTemplateByQuery()
		{
			var result = this.Client.SearchTemplate<ElasticsearchProject>(s => s
				.Template(_template)
				.Params(p => p
					.Add("my_from", 0)
					.Add("my_size", 5)
					.Add("my_field", "name")
					.Add("my_value", "em-elasticsearch")
				)
			);

			result.IsValid.Should().BeTrue();
			result.Hits.Count().Should().BeGreaterThan(0);
		}

		[Test]
		[SkipVersion("0 - 1.0.9", "Search template API added in 1.1")]
		public void SearchTemplateByQuery_ObjectInitializer()
		{
			var request = new SearchTemplateRequest
			{
				Template = _template,
				Params = new Dictionary<string, object>
				{
					{ "my_from", 0 },
					{ "my_size", 5 },
					{ "my_field", "name" },
					{ "my_value", "em-elasticsearch" }
				}
			};

			var result = this.Client.SearchTemplate<ElasticsearchProject>(request);

			result.IsValid.Should().BeTrue();
			result.Hits.Count().Should().BeGreaterThan(0);
		}

		[Test]
		[SkipVersion("0 - 1.2.9", "Put search template introduced in 1.3")]
		public void SearchTemplateById()
		{
			var templateName = "myIndexedTemplate";

			var putTemplateResult = this.Client.PutSearchTemplate(templateName, t => t.Template(_template));
			putTemplateResult.IsValid.Should().BeTrue();

			var result = this.Client.SearchTemplate<ElasticsearchProject>(s => s
				.Id(templateName)
				.Params(p => p
					.Add("my_from", 0)
					.Add("my_size", 5)
					.Add("my_field", "name")
					.Add("my_value", "em-elasticsearch")
				)
			);

			result.IsValid.Should().BeTrue();
			result.Hits.Count().Should().BeGreaterThan(0);

			var deleteTemplateResult = this.Client.DeleteSearchTemplate(templateName);
			deleteTemplateResult.IsValid.Should().BeTrue();
		}

		[Test]
		[SkipVersion("0 - 1.2.9", "Put search template introduced in 1.3")]
		public void PutGetAndDeleteTemplate()
		{
			var templateName = "myIndexedTemplate";

			var putResult = this.Client.PutSearchTemplate(templateName, t => t.Template(_template));
			putResult.IsValid.Should().BeTrue();

			var getResult = this.Client.GetSearchTemplate(templateName);
			getResult.IsValid.Should().BeTrue();
			getResult.Template.ShouldBeEquivalentTo(_template);
			
			var deleteResult = this.Client.DeleteSearchTemplate(templateName);
			deleteResult.IsValid.Should().BeTrue();
		}
	}
}

