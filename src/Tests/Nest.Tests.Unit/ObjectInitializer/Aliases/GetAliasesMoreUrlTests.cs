﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest17.Tests.Unit.ObjectInitializer.Aliases
{
	[TestFixture]
	public class GetAliasesMoreUrlTests : BaseJsonTests
	{

		[Test]
		public void NoIndexAndAlias()
		{
			var request = new GetAliasesRequest();
			var response = this._client.GetAliases(request);
			
			var status = response.ConnectionStatus;
		
			//MONO uriencodes *, functionally equivalent so we do a 'dirty' fix here.
			if (Type.GetType("Mono.Runtime") != null)
				status.RequestUrl.Should().EndWith("/_aliases/%2A");
			else 
				status.RequestUrl.Should().EndWith("/_aliases/*");
			status.RequestMethod.Should().Be("GET");
		}

		[Test]
		public void IndexAndAlias()
		{
			var request = new GetAliasesRequest()
			{
				Indices = new IndexNameMarker[] { "my-index" } ,
				Alias = "my-alias"  
			};
			var response = this._client.GetAliases(request);
			
			var status = response.ConnectionStatus;
		
			status.RequestUrl.Should().EndWith("/my-index/_aliases/my-alias");
			status.RequestMethod.Should().Be("GET");
		}
	}
}
