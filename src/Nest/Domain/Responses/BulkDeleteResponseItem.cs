﻿using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject]
	[JsonConverter(typeof(BulkOperationResponseItemConverter))]
	public class BulkDeleteResponseItem : BulkOperationResponseItem
	{
		public override string Operation { get; internal set; }
		[JsonProperty("_index")]
		public override string Index { get; internal set; }
		[JsonProperty("_type")]
		public override string Type { get; internal set; }
		[JsonProperty("_id")]
		public override string Id { get; internal set; }
		[JsonProperty("_version")]
		public override string Version { get; internal set; }
		[JsonProperty("status")]
		public override int Status { get; internal set; }
		[JsonProperty("found")]
		public bool Found { get; internal set; }
		[JsonProperty("error")]
		public override string Error { get; internal set; }
	}
}