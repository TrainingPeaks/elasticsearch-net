using System;
using ES.Net;
using FluentAssertions;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Integration.Core.Map
{
	[TestFixture]
	public abstract class BaseMappingTests : IntegrationTests
	{
		[TestFixtureSetUp]
		public void Initialize()
		{
			this.Client.DeleteMapping<ElasticsearchProject>();
		}

		protected void DefaultResponseAssertations(IIndicesResponse result)
		{
			result.Should().NotBeNull();
			if (!result.IsValid)
				throw new Exception(result.ConnectionStatus.ResponseRaw.Utf8String());
			result.IsValid.Should().BeTrue();
		}
	}
}
