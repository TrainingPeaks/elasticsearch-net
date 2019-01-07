﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using FluentAssertions;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Unit.ObjectInitializer.Exists
{
	[TestFixture]
	public class DocumentExistsRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public DocumentExistsRequestTests()
		{
			var request = new DocumentExistsRequest(typeof(ElasticsearchProject), "hello-world", "3");
			var response = this._client.DocumentExists(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/nest_test_data/hello-world/3");
			this._status.RequestMethod.Should().Be("HEAD");
		}
	
	}
}
