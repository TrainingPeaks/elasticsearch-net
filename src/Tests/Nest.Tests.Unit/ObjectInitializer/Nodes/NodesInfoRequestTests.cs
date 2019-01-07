using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest17.Tests.Unit.ObjectInitializer.Nodes
{
	[TestFixture]
	public class NodesInfoRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public NodesInfoRequestTests()
		{
			var request = new NodesInfoRequest
			{
				NodeId = "my-node-id",
				Human = true,
				FlatSettings = true
			};
			var response = this._client.NodesInfo(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_nodes/my-node-id?human=true&flat_settings=true");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}
