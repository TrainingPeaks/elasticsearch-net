﻿using System.Collections.Generic;
namespace Nest17
{
	public abstract class BucketAggregationBase : AggregationsHelper , IBucketAggregation
	{
		protected BucketAggregationBase() { }
		protected BucketAggregationBase(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }
	}
}