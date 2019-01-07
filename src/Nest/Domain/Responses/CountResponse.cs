﻿using Newtonsoft.Json;

namespace Nest17
{
	public interface ICountResponse : IResponse
	{
		long Count { get; }
		ShardsMetaData Shards { get; }
	}

	[JsonObject]
	public class CountResponse : BaseResponse, ICountResponse
	{
		public CountResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "count")]
		public long Count { get; internal set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }
	}
}