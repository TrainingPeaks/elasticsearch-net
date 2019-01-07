using System;
using Nest17.Resolvers.Converters;
using Nest17.Resolvers.Converters.Aggregations;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FilterAggregatorConverter))]
	public interface IFilterAggregator : IBucketAggregator
	{
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }
	}

	public class FilterAggregator : BucketAggregator, IFilterAggregator
	{
		public IFilterContainer Filter { get; set; }
	}

	public class FilterAggregationDescriptor<T> : BucketAggregationBaseDescriptor<FilterAggregationDescriptor<T>, T> , IFilterAggregator 
		where T : class
	{
		IFilterContainer IFilterAggregator.Filter { get; set; }

		public FilterAggregationDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> selector)
		{
			((IFilterAggregator)this).Filter = selector(new FilterDescriptor<T>());
			return this;
		}


	}
}