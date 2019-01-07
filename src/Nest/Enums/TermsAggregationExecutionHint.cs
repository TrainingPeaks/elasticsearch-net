﻿using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Nest17
{
	/// <summary>
	/// Determines how the terms aggregation is executed
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TermsAggregationExecutionHint
	{
		/// <summary>
		/// Order by using field values directly in order to aggregate data per-bucket 
		/// </summary>
		[EnumMember(Value = "map")]
		Map,
		/// <summary>
		/// Order by using ordinals of the field values instead of the values themselves
		/// </summary>
		[Obsolete("ordinals was removed from ES in version 1.3.  Use global_ordinals instead.")]
		[EnumMember(Value = "ordinals")]
		Ordinals,
		/// <summary>
		/// Order by using ordinals of the field and preemptively allocating one bucket per ordinal value
		/// </summary>
		[EnumMember(Value = "global_ordinals")]
		GlobalOrdinals,
		/// <summary>
		/// Order by using ordinals of the field and dynamically allocating one bucket per ordinal value
		/// </summary>
		[EnumMember(Value = "global_ordinals_hash")]
		GlobalOrdinalsHash,
		/// <summary>
		/// Order by using per-segment ordinals to compute counts and remap these counts to global counts using global ordinals
		/// </summary>
		[EnumMember(Value = "global_ordinals_low_cardinality")]
		GlobalOrdinalsLowCardinality
	}
}