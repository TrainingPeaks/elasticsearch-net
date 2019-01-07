﻿using ES.Net;
using NUnit.Framework;

namespace Nest17.Tests.Integration.Cluster
{
	[TestFixture]
	public class HealthTests : IntegrationTests
	{
		[Test]
		public void ClusterHealth()
		{
			var r = this.Client.ClusterHealth(h=>h.Level(Level.Cluster));
			Assert.True(r.IsValid);
		}
		[Test]
		public void ClusterHealthPerIndex()
		{
			var r = this.Client.ClusterHealth(h=>h.Index(ElasticsearchConfiguration.DefaultIndex).Level(Level.Cluster));
			Assert.True(r.IsValid);
		}
		[Test]
		public void IndexHealth()
		{
			var r = this.Client.ClusterHealth(h=>h.Level(Level.Indices));
			Assert.True(r.IsValid);
		}
		[Test]
		public void ShardHealth()
		{
			var r = this.Client.ClusterHealth(h=>h.Level(Level.Shards));
			Assert.True(r.IsValid);
		}
		[Test]
		public void DetailedHealth()
		{
			var r = this.Client.ClusterHealth(h => h
				.Level(Level.Shards)
				.Timeout("30s")
				.WaitForNodes("1")
				.WaitForRelocatingShards(0)
			);
			Assert.True(r.IsValid);
		}
		[Test]
		public void DetailedHealthPerIndex()
		{
			var r = this.Client.ClusterHealth(h => h
				.Indices(ElasticsearchConfiguration.DefaultIndex)
				.Level(Level.Shards)
				.Timeout("30s")
				.WaitForNodes("1")
				.WaitForRelocatingShards(0)
			);
			Assert.True(r.IsValid);
		}
	}
}