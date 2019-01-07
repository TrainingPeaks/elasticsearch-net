﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace Nest17.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce487Tests : IntegrationTests
	{
		public class Post
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}


		/// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/487
		/// </summary>
		[Test]
		public void NoSearchResults()
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var x = this.Client.CreateIndex(index, s => s
				.AddMapping<ElasticsearchProject>(m => m
					.Properties(pp=>pp
						.String(sm=>sm.Name(p=>p.Name).Store())
					)
				)
			);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());

			var typeMapping = this.Client.GetMapping<ElasticsearchProject>(i => i.Index(index).Type("elasticsearchprojects"));
			typeMapping.Should().NotBeNull();
			var stringMapping = typeMapping.Mapping.Properties["name"] as StringMapping;
			stringMapping.Should().NotBeNull();
			stringMapping.Store.Should().HaveValue();
			stringMapping.Store.Should().BeTrue();
		}

	}
}
