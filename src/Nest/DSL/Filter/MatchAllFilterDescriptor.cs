using System;
using System.Collections.Generic;
using System.Linq;
using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonConverter(typeof(ReadAsTypeConverter<MatchAllFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMatchAllFilter : IFilter
	{
	}
	
	public class MatchAllFilter : PlainFilter, IMatchAllFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.MatchAll = this;
		}
	}

	public class MatchAllFilterDescriptor : FilterBase, IMatchAllFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return false;
			}

		}
	}
}
