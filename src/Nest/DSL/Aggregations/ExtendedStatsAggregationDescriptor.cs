using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<ExtendedStatsAggregator>))]
	public interface IExtendedStatsAggregator : IMetricAggregator
	{
	}

	public class ExtendedStatsAggregator : MetricAggregator, IExtendedStatsAggregator { }

	public class ExtendedStatsAggregationDescriptor<T> : MetricAggregationBaseDescriptor<ExtendedStatsAggregationDescriptor<T>, T>, IExtendedStatsAggregator where T : class
	{
		
	}
}