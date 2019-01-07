﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest17
{
	public class SingleBucket : BucketAggregationBase
	{
		public SingleBucket() { }

		public SingleBucket(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }

		[JsonProperty("doc_count")]
		public long DocCount { get; set; }
	}
}