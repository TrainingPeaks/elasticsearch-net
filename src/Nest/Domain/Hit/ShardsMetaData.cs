﻿using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest17
{
    [JsonObject]
    public class ShardsMetaData
    {
        [JsonProperty]
        public int Total { get; internal set; }

        [JsonProperty]
        public int Successful { get; internal set; }

        [JsonProperty]
        public int Failed { get; internal set; }

        [JsonProperty("failures")]
        public IEnumerable<ShardsFailureReason> Failures { get; internal set; }
    }

	[JsonObject]
	public class ShardsFailureReason
	{
		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("shard")]
		public int Shard { get; internal set; }

		[JsonProperty("status")]
		public int Status { get; internal set; }

		[JsonProperty("reason")]
		public string Reason { get; internal set; }

	}
}