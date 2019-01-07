﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ES.Net.Connection;
using FluentAssertions;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce1440Tests : BaseJsonTests
	{
		[Test]
		public void CountShouldNotThrowWhenNoDefaultIndexSpecified()
		{
			var client = new ElasticClient(connection: new InMemoryConnection(new ConnectionSettings()));
			var request = client.Count<ElasticsearchProject>();
			var path = new Uri(request.ConnectionStatus.RequestUrl).AbsolutePath;
			path.Should().Be("/_all/elasticsearchprojects/_count");
			request = client.Count<ElasticsearchProject>(c=>c.Index("x"));
			path = new Uri(request.ConnectionStatus.RequestUrl).AbsolutePath;
			path.Should().Be("/x/elasticsearchprojects/_count");
		}

	}
}
