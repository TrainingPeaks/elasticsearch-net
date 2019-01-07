﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	public interface IClusterStatsResponse : IResponse
	{
		string ClusterName { get; set; }
		string Status { get; set; }
		IndicesStats Indices { get; set; }
		ClusterNodesStats Nodes { get; set; }
	}

	[JsonObject]
	public class ClusterStatsResponse : BaseResponse, IClusterStatsResponse
	{
		[JsonProperty("cluster_name")]
		public string ClusterName { get; set; }
		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("indices")]
		public IndicesStats Indices { get; set; }

		[JsonProperty("nodes")]
		public ClusterNodesStats Nodes { get; set; }
	}
}
