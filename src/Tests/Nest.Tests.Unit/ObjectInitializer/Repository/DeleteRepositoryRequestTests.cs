using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest17.Tests.Unit.ObjectInitializer.Repository
{
	[TestFixture]
	public class DeleteRepositoryRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public DeleteRepositoryRequestTests()
		{
			var request = new DeleteRepositoryRequest("my-repository");
			var response = this._client.DeleteRepository(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_snapshot/my-repository");
			this._status.RequestMethod.Should().Be("DELETE");
		}
	}

}
