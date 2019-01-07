﻿using ES.Net;
using FluentAssertions;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nest17.Tests.Unit.ObjectInitializer.Update
{
	public class UpdateRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;
		public UpdateRequestTests()
		{
			var project = new ElasticsearchProject { Id = 1 };

			var request = new UpdateRequest<ElasticsearchProject, object>(project)
				{
					Doc = new { Name = "NEST" },
					DocAsUpsert = true
				};

			var response = this._client.Update(request);
			_status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_update");
			this._status.RequestMethod.Should().Be("POST");
		}

		[Test]
		public void UpdateBody()
		{
			this.JsonEquals(this._status.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
