﻿using System.Linq;
using Nest17.Tests.MockData;
using Nest17.Tests.MockData.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace Nest17.Tests.Integration.Search
{
	[TestFixture]
	public class CountTests : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;


		[Test]
		public void SimpleCount()
		{
			var countResults = this.Client.Count<dynamic>(c => c.AllIndices().AllTypes().Query(q => q.MatchAll()));
			Assert.True(countResults.Count > 0);
		}

		[Test]
		public void SimpleQueryCount()
		{
			var countResults = this.Client.Count<ElasticsearchProject>(c => c
			.Query(q => q
				.Fuzzy(fq => fq
					.Value(this._LookFor.ToLowerInvariant())
					.OnField(f => f.Followers.First().FirstName)
				)
			));
			Assert.True(countResults.Count > 0);
		}

		[Test]
		public void SimpleQueryWithIndexAndTypeCount()
		{
			//does a match_all on the default specified index
			var countResults = this.Client.Count<ElasticsearchProject>(c => c
			.Query(q => q
				.Fuzzy(fq => fq
					.PrefixLength(4)
					.OnField(f => f.Followers.First().FirstName)
					.Value(this._LookFor.ToLowerInvariant())
				)
			));
			countResults.Count.Should().BeGreaterThan(0);
		}

		[Test]
		public void SimpleQueryWithIndicesCount()
		{
			//does a match_all on the default specified index
			var index = ElasticsearchConfiguration.DefaultIndex;
			var indices = new[] { index, index + "_clone" };
			var types = new[] { this.Client.Infer.TypeName<ElasticsearchProject>() };
			var countResults = this.Client.Count<ElasticsearchProject>(c => c
				.Indices(indices).Types(types)
				.Query(q => q
					.Fuzzy(fq => fq
						.PrefixLength(4)
						.OnField(f => f.Followers.First().FirstName)
						.Value(this._LookFor.ToLowerInvariant())
					)
				)
			);
			countResults.IsValid.Should().Be(true);
			countResults.Count.Should().BeGreaterThan(0);
		}

		[Test]
		public void SimpleTypedCount()
		{
			var countResults = this.Client.Count<ElasticsearchProject>(c => c.Query(q => q.MatchAll()));

			Assert.True(countResults.Count > 0);
		}

	}
}