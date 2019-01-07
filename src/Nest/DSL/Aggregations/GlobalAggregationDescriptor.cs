using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<GlobalAggregator>))]
	public interface IGlobalAggregator : IBucketAggregator
	{
		
	}

	public class GlobalAggregator : BucketAggregator, IGlobalAggregator
	{
		
	}

	public class GlobalAggregationDescriptor<T> : BucketAggregationBaseDescriptor<GlobalAggregationDescriptor<T>, T>, IGlobalAggregator
		where T : class
	{
	}
}