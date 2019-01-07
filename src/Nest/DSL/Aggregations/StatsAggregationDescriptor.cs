using System;
using System.Collections.Generic;
using System.Linq;
using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<StatsAggregator>))]
	public interface IStatsAggregator : IMetricAggregator
	{
	}

	public class StatsAggregator : MetricAggregator, IStatsAggregator
	{
	}

	public class StatsAggregationDescriptor<T> : MetricAggregationBaseDescriptor<StatsAggregationDescriptor<T>, T>, IStatsAggregator where T : class
	{
		
	}
}
