﻿using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClusterHealthResponse : IResponse
	{
		int ActivePrimaryShards { get; }
		int ActiveShards { get; }
		string ClusterName { get; }
		IReadOnlyDictionary<IndexName, IndexHealthStats> Indices { get; }
		int InitializingShards { get; }
		int NumberOfDataNodes { get; }
		int NumberOfNodes { get; }
		int NumberOfPendingTasks { get; }
		int RelocatingShards { get; }
		Health Status { get; }
		bool TimedOut { get; }
		int UnassignedShards { get; }
	}

	[JsonObject]
	public class ClusterHealthResponse : ResponseBase, IClusterHealthResponse
	{
		[JsonProperty("active_primary_shards")]
		public int ActivePrimaryShards { get; internal set; }

		[JsonProperty("active_shards")]
		public int ActiveShards { get; internal set; }

		[JsonProperty("cluster_name")]
		public string ClusterName { get; internal set; }

		[JsonProperty("indices")]
		[JsonConverter(typeof(ResolvableDictionaryJsonConverter<IndexName, IndexHealthStats>))]
		public IReadOnlyDictionary<IndexName, IndexHealthStats> Indices { get; internal set; } =
			EmptyReadOnly<IndexName, IndexHealthStats>.Dictionary;

		[JsonProperty("initializing_shards")]
		public int InitializingShards { get; internal set; }

		[JsonProperty("number_of_data_nodes")]
		public int NumberOfDataNodes { get; internal set; }

		[JsonProperty("number_of_nodes")]
		public int NumberOfNodes { get; internal set; }

		[JsonProperty(PropertyName = "number_of_pending_tasks")]
		public int NumberOfPendingTasks { get; internal set; }

		[JsonProperty("relocating_shards")]
		public int RelocatingShards { get; internal set; }

		[JsonProperty("status")]
		public Health Status { get; internal set; }

		[JsonProperty("timed_out")]
		public bool TimedOut { get; internal set; }

		[JsonProperty("unassigned_shards")]
		public int UnassignedShards { get; internal set; }
	}
}
