﻿using System;
using ES.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBulkOperation
	{
		string Operation { get; }
		Type ClrType { get; }

		[JsonProperty(PropertyName = "_index")]
		IndexNameMarker Index { get; set; }

		[JsonProperty(PropertyName = "_type")]
		TypeNameMarker Type { get; set; }

		[JsonProperty(PropertyName = "_id")]
		string Id { get; set; }

		[JsonProperty(PropertyName = "_version")]
		string Version { get; set; }

		[JsonProperty(PropertyName = "_version_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		VersionType? VersionType { get; set; }

		[JsonProperty(PropertyName = "_routing")]
		string Routing { get; set; }

		[JsonProperty(PropertyName = "_parent")]
		string Parent { get; set; }

		[JsonProperty("_timestamp")]
		long? Timestamp { get; set; }

		[JsonProperty("_ttl")]
		string Ttl { get; set; }

		[JsonProperty("_retry_on_conflict")]
		int? RetriesOnConflict { get; set; }

		object GetBody();

		string GetIdForOperation(ElasticInferrer inferrer);
	}
}