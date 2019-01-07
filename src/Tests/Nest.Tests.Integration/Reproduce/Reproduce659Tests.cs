﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace Nest17.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce659Tests : IntegrationTests
	{

		/// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/659
		/// </summary>
		[Test]
		public void ShouldNotThrowOnInvalidConnectionWithoutRequestConfig()
		{
			var searchResult = this.Client.Search<ElasticsearchProject>(s => s
				.Index("this_index_does_not_exist")
				.Size(0)
				.Aggregations(aggs=>aggs
					.Terms("numericTerms", t=>t
						.Field(p=>p.LongValue)
						.Size(10)
					)
				)
			);

			searchResult.IsValid.Should().BeFalse();
			searchResult.ConnectionStatus.HttpStatusCode.Should().Be(404);
			var e = searchResult.ConnectionStatus.OriginalException as ElasticsearchServerException;
			e.Should().NotBeNull();
			e.ExceptionType.Should().Be("IndexMissingException");

		}

	}
}
