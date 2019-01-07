﻿using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Integration.Core
{
	[TestFixture]
	public class AsyncTests : IntegrationTests
	{

		[Test]
		public async void TestIndex()
		{
			var newProject = new ElasticsearchProject
			{
				Name = "COBOLES", //COBOL ES client ?
			};
			var t = await  this.Client.IndexAsync<ElasticsearchProject>(newProject);
			Assert.True(t.IsValid);
		}
		
	}
}
