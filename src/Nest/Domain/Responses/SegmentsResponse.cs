﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest17
{
	public interface ISegmentsResponse : IResponse
	{
		ShardsMetaData Shards { get; }
		Dictionary<string, IndexSegment> Indices { get; set; }
	}

	[JsonObject]
	public class SegmentsResponse : BaseResponse, ISegmentsResponse
	{

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "indices")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, IndexSegment> Indices { get; set; } 

		
	}
}