﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ES.Net;
using ES.Net.Connection;
using FluentAssertions;
using Nest17.Tests.MockData.Domain;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Nest17.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce1146Tests : BaseJsonTests
	{
		[Test]
		public void DeleteByQueryInference()
		{
			var ids = new[] {"1", "2", "3"};
			IResponse response = this._client.DeleteByQuery<string>(s => s
				.Query(q => q.Terms("string.ProgramId", ids))
			);
			var status = response.ConnectionStatus;
			status.RequestUrl.Should().EndWith("/nest_test_data/string/_query");
			
			response = this._client.DeleteByQuery<JObject>(s => s
				.Query(q => q.Terms("string.ProgramId", ids))
			);
			status = response.ConnectionStatus;
			status.RequestUrl.Should().EndWith("/nest_test_data/jobject/_query");

			var client = new ElasticClient(
				new ConnectionSettings(defaultIndex: "nest_test_data"),
				connection: new InMemoryConnection());
			
			response = client.DeleteByQuery<string>(s => s
				.Query(q => q.Terms("string.ProgramId", ids))
			);

			status = response.ConnectionStatus;
			status.RequestUrl.Should().EndWith("/nest_test_data/string/_query");
			
			response = client.DeleteByQuery<JObject>(s => s
				.Query(q => q.Terms("string.ProgramId", ids))
			);
			status = response.ConnectionStatus;
			status.RequestUrl.Should().EndWith("/nest_test_data/jobject/_query");
		}
	}
}
