using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using FluentAssertions;
using Nest17.Resolvers;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Unit.ObjectInitializer.Analyze
{
	[TestFixture]
	public class AnalyzeRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public AnalyzeRequestTests()
		{
			var request = new AnalyzeRequest("analyze this text")
			{
				Indices = new IndexNameMarker[] { typeof(ElasticsearchProject)}, 
				Field = Property.Path<ElasticsearchProject>(p=>p.Name)
			};
			var response = this._client.Analyze(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/nest_test_data/_analyze?field=name");
			this._status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void AnalyzeBody()
		{
			this._status.Request.Utf8String().Should().Be("analyze this text");
		}
	
	}
}
