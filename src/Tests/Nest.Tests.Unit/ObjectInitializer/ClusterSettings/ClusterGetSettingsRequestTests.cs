﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest17.Tests.Unit.ObjectInitializer.ClusterHealth
{
	[TestFixture]
	public class ClusterGetSettingsRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public ClusterGetSettingsRequestTests()
		{
			var request = new ClusterGetSettingsRequest
			{
				FlatSettings = true,

			};
			var response = this._client.ClusterGetSettings(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_cluster/settings?flat_settings=true");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}
