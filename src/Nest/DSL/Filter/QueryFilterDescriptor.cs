﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nest17.Resolvers.Converters;

namespace Nest17
{
	[JsonConverter(typeof(ReadAsTypeConverter<QueryFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IQueryFilter : IFilter
	{
		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }
	}

	public class QueryFilter : PlainFilter, IQueryFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Query = this;
		}

		public IQueryContainer Query { get; set; }
	}

	public class QueryFilterDescriptor : FilterBase, IQueryFilter
	{

		IQueryContainer IQueryFilter.Query { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				var af = ((IQueryFilter)this);
				return af.Query == null || af.Query.IsConditionless;
			} 
		}
	}
}
