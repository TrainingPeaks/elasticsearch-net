﻿using System;
using System.Net;
using ES.Net.Connection;
using ES.Net.ConnectionPool;
using FluentAssertions;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Unit.Search.Suggest
{
	[TestFixture]
	public class SuggestUrlTest
	{
		private void TestUrl(string expected, Func<SuggestDescriptor<ElasticsearchProject>, SuggestDescriptor<ElasticsearchProject>>  descriptor, ConnectionSettings settings = null)
		{
			var client = new ElasticClient(settings, new InMemoryConnection());
			var response = client.Suggest(descriptor);
			var uri = new Uri(response.ConnectionStatus.RequestUrl);
			var actual = WebUtility.UrlDecode(uri.AbsolutePath);
			actual.Should().Be(expected);
		}

		[Test]
		public void Suggest_to_all_url_test()
		{
			TestUrl(
				expected: "/_all/_suggest",
				descriptor: d => d.Term("name", s => s.OnField(f => f.Contributors)).AllIndices()
			);
		}

		[Test]
		public void Suggest_to_specific_index_url_test()
		{
			var settings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://esserver.com")));

			settings.MapDefaultTypeIndices(d => d
				.Add(typeof (ElasticsearchProject), typeof (ElasticsearchProject).Name.ToLower()));

			TestUrl(
				expected: "/elasticsearchproject/_suggest",
				descriptor: d => d.Term("name", s => s.OnField(f => f.Contributors)),
				settings: settings
			);
		}

		[Test]
		public void Suggest_to_specific_index_specified_in_descriptor_url_test()
		{
			var settings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://esserver.com")));

			settings.MapDefaultTypeIndices(d => d
				.Add(typeof (ElasticsearchVersionInfo), "elasticsearchproject"));

			TestUrl(
				expected: "/elasticsearchproject/_suggest",
				descriptor: d => d.Term("name", s => s.OnField(f => f.Contributors)).Index<ElasticsearchVersionInfo>(),
				settings: settings
			);
		}

		[Test]
		public void Suggest_to_specific_index_specified_as_string_in_descriptor_url_test()
		{
			var settings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://esserver.com")));

			TestUrl(
				expected: "/anotherindex/_suggest",
				descriptor: d => d.Term("name", s => s.OnField(f => f.Contributors)).Index("anotherindex")
			);
		}
	}
}
