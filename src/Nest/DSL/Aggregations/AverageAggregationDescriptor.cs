using System;
using System.Collections.Generic;
using System.Linq;
using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<AverageAggregator>))]
	public interface IAverageAggregator : IMetricAggregator
	{
	}

	public class AverageAggregator : MetricAggregator, IAverageAggregator
	{
		
	}

	public class AverageAggregationDescriptor<T> : MetricAggregationBaseDescriptor<AverageAggregationDescriptor<T>, T>, IAverageAggregator where T : class
	{
		
	}
}
