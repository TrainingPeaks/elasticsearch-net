using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<HistogramAggregator>))]
	public interface IHistogramAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("params")]
		FluentDictionary<string, object> Params { get; set; }

		[JsonProperty("interval")]
		double? Interval { get; set; }

		[JsonProperty("min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		[JsonProperty("order")]
		IDictionary<string, string> Order { get; set; }

		[JsonProperty("extended_bounds")]
		IDictionary<string, object> ExtendedBounds { get; set; }

		[JsonProperty("pre_offset")]
		long? PreOffset { get; set; }

		[JsonProperty("post_offset")]
		long? PostOffset { get; set; }
	}

	public class HistogramAggregator : BucketAggregator, IHistogramAggregator
	{
		public PropertyPathMarker Field { get; set; }
		public string Script { get; set; }
		public FluentDictionary<string, object> Params { get; set; }
		public double? Interval { get; set; }
		public int? MinimumDocumentCount { get; set; }
		public IDictionary<string, string> Order { get; set; }
		public IDictionary<string, object> ExtendedBounds { get; set; }
		public long? PreOffset { get; set; }
		public long? PostOffset { get; set; }
	}

	public class HistogramAggregationDescriptor<T> : BucketAggregationBaseDescriptor<HistogramAggregationDescriptor<T>, T>, IHistogramAggregator 
		where T : class
	{
		private IHistogramAggregator Self { get { return this; } }

		PropertyPathMarker IHistogramAggregator.Field { get; set; }
		
		string IHistogramAggregator.Script { get; set; }

		FluentDictionary<string, object> IHistogramAggregator.Params { get; set; }

		double? IHistogramAggregator.Interval { get; set; }

		int? IHistogramAggregator.MinimumDocumentCount { get; set; }

		IDictionary<string, string> IHistogramAggregator.Order { get; set; }

		IDictionary<string, object> IHistogramAggregator.ExtendedBounds { get; set; }

		long? IHistogramAggregator.PreOffset { get; set; }

		long? IHistogramAggregator.PostOffset { get; set; }
		
		public HistogramAggregationDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public HistogramAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public HistogramAggregationDescriptor<T> Script(string script)
		{
			Self.Script = script;
			return this;
		}

		public HistogramAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			Self.Params = paramSelector(new FluentDictionary<string, object>());
			return this;
		}

		public HistogramAggregationDescriptor<T> Interval(double interval)
		{
			Self.Interval = interval;
			return this;
		}
		
		public HistogramAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount)
		{
			Self.MinimumDocumentCount = minimumDocumentCount;
			return this;
		}
		
		public HistogramAggregationDescriptor<T> OrderAscending(string key)
		{
			Self.Order = new Dictionary<string, string> { {key, "asc"}};
			return this;
		}
	
		public HistogramAggregationDescriptor<T> OrderDescending(string key)
		{
			Self.Order = new Dictionary<string, string> { {key, "asc"}};
			return this;
		}

		public HistogramAggregationDescriptor<T> ExtendedBounds(double min, double max)
		{
			Self.ExtendedBounds = new Dictionary<string, object> { { "min", min }, { "max", max } };
			return this;
		}

		public HistogramAggregationDescriptor<T> PreOffset(long preOffset)
		{
			Self.PreOffset = preOffset;
			return this;
		}

		public HistogramAggregationDescriptor<T> PostOffset(long postOffset)
		{
			Self.PostOffset = postOffset;
			return this;
		}
	}
}