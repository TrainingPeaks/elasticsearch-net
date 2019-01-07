﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using FluentAssertions;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest17.Tests.Integration.Core
{
	[TestFixture]
	public class IndexTests : IntegrationTests
	{
		private readonly Random idGen = new Random();

		[Test]
		public void IndexUsingCreateFlag()
		{
			// Document to be indexed.
			var doc = new ElasticsearchProject
			{
				Country = "Mozambique",
				Followers = new List<Person>(),
				Id = idGen.Next(),
				Name = "Test Document for 'IndexDocument' Create Flag"
			};

			// Index the document
			var indexResult = this.Client.Index<ElasticsearchProject>(doc, i => i.OpType(OpType.Create));
			indexResult.IsValid.Should().BeTrue();

			// Grab the indexed document.
			var foundDoc = this.Client.Source<ElasticsearchProject>(doc.Id);

			// Check that the document was successfully indexed.
			Assert.NotNull(foundDoc);
			Assert.AreEqual(doc.Country, foundDoc.Country);
			Assert.AreEqual(doc.Followers.Count, foundDoc.Followers.Count);
			Assert.AreEqual(doc.Id, foundDoc.Id);
			Assert.AreEqual(doc.Name, foundDoc.Name);

			// Now try to index the document again while using the Create Flag
			var response = this.Client.Index<ElasticsearchProject>(doc, i => i.OpType(OpType.Create));

			// Make sure the index request failed with HTTP status 409 since document with same id already exists.
			Assert.False(response.Created);
			Assert.AreEqual(409, response.ConnectionStatus.HttpStatusCode);
		}
		
		[Test]
		public void IndexUsingCreateFlagOnNoRawResponseClient()
		{
			// Document to be indexed.
			var doc = new ElasticsearchProject
			{
				Country = "Mozambique",
				Followers = new List<Person>(),
				Id = idGen.Next(),
				Name = "Test Document for 'IndexDocument' Create Flag"
			};

			// Index the document
			var indexResult = this.ClientNoRawResponse.Index<ElasticsearchProject>(doc, i => i.OpType(OpType.Create));
			indexResult.IsValid.Should().BeTrue();

			// Grab the indexed document.
			var foundDoc = this.ClientNoRawResponse.Source<ElasticsearchProject>(doc.Id);

			// Check that the document was successfully indexed.
			Assert.NotNull(foundDoc);
			Assert.AreEqual(doc.Country, foundDoc.Country);
			Assert.AreEqual(doc.Followers.Count, foundDoc.Followers.Count);
			Assert.AreEqual(doc.Id, foundDoc.Id);
			Assert.AreEqual(doc.Name, foundDoc.Name);

			// Now try to index the document again while using the Create Flag
			var response = this.ClientNoRawResponse.Index<ElasticsearchProject>(doc, i => i.OpType(OpType.Create));

			// Make sure the index request failed with HTTP status 409 since document with same id already exists.
			Assert.False(response.Created);
			Assert.AreEqual(409, response.ConnectionStatus.HttpStatusCode);
		}
	}
}
