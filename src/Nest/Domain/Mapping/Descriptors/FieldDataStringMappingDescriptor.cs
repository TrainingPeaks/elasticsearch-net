﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	public class FieldDataStringMappingDescriptor
	{
		internal FieldDataStringMapping FieldData { get; private set; }

		public FieldDataStringMappingDescriptor()
		{
			this.FieldData = new FieldDataStringMapping();
		}
		
		public FieldDataStringMappingDescriptor Format(FieldDataStringFormat format)
		{
			this.FieldData.Format = format;
			return this;
		}

		public FieldDataStringMappingDescriptor Loading(FieldDataLoading loading)
		{
			this.FieldData.Loading = loading;
			return this;
		}

		public FieldDataStringMappingDescriptor Filter(Func<FieldDataFilterDescriptor, FieldDataFilterDescriptor> filterSelector)
		{
			var selector = filterSelector(new FieldDataFilterDescriptor());
			this.FieldData.Filter = selector.Filter;
			return this;
		}
	}
}
