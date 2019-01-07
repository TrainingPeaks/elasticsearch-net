﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<FacetContainer>))]
	public interface IFacetContainer
	{
		[JsonProperty(PropertyName = "global")]
		bool? Global { get; set; }

		[JsonProperty(PropertyName = "nested")]
		PropertyPathMarker Nested { get; set; }

		[JsonProperty(PropertyName = "terms")]
		ITermFacetRequest Terms { get; set; }

		[JsonProperty(PropertyName = "range")]
		IFacetRequest Range { get; set; }

		[JsonProperty(PropertyName = "histogram")]
		IHistogramFacetRequest Histogram { get; set; }

		[JsonProperty(PropertyName = "date_histogram")]
		IDateHistogramFacetRequest DateHistogram { get; set; }

		[JsonProperty(PropertyName = "query")]
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }

		[JsonProperty(PropertyName = "statistical")]
		IStatisticalFacetRequest Statistical { get; set; }

		[JsonProperty(PropertyName = "terms_stats")]
		ITermsStatsFacetRequest TermsStats { get; set; }

		[JsonProperty(PropertyName = "geo_distance")]
		IGeoDistanceFacetRequest GeoDistance { get; set; }

		[JsonProperty(PropertyName = "facet_filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IFilterContainer FacetFilter { get; set; }
	}

	public class FacetContainer : IFacetContainer 
	{
		[JsonProperty(PropertyName = "global")]
		public bool? Global { get; set; }

		[JsonProperty(PropertyName = "nested")]
		public PropertyPathMarker Nested { get; set; }

		[JsonProperty(PropertyName = "terms")]
		public ITermFacetRequest Terms { get; set; }

		[JsonProperty(PropertyName = "range")]
		public IFacetRequest Range { get; set; }

		[JsonProperty(PropertyName = "histogram")]
		public IHistogramFacetRequest Histogram { get; set; }

		[JsonProperty(PropertyName = "date_histogram")]
		public IDateHistogramFacetRequest DateHistogram { get; set; }

		[JsonProperty(PropertyName = "query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		public IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		public IFilterContainer Filter { get; set; }

		[JsonProperty(PropertyName = "statistical")]
		public IStatisticalFacetRequest Statistical { get; set; }

		[JsonProperty(PropertyName = "terms_stats")]
		public ITermsStatsFacetRequest TermsStats { get; set; }

		[JsonProperty(PropertyName = "geo_distance")]
		public IGeoDistanceFacetRequest GeoDistance { get; set; }

		[JsonProperty(PropertyName = "facet_filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		public IFilterContainer FacetFilter { get; set; }
	}
}
