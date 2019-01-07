﻿using Newtonsoft.Json;
using System.Collections.Generic;
using Nest17.Resolvers.Converters;

namespace Nest17
{
    [JsonObject]
    [JsonConverter(typeof(ShardsSegmentConverter))]
    public class ShardsSegment
    {
        [JsonProperty(PropertyName = "num_committed_segments")]
        public int CommittedSegments { get; internal set; }

        [JsonProperty(PropertyName = "num_search_segments")]
        public int SearchSegments { get; internal set; }

        [JsonProperty(PropertyName = "routing")]
        public ShardSegmentRouting Routing { get; internal set; }

        [JsonProperty]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, Segment> Segments { get; internal set; }

    }
}