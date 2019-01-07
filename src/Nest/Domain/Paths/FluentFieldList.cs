﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest17
{
	public class FluentFieldList<T> : List<PropertyPathMarker> where T : class
	{
		public FluentFieldList<T> Add(Expression<Func<T, object>> k)
		{
			base.Add(k);
			return this;
		}
		public FluentFieldList<T> Add(string k)
		{
			base.Add(k);
			return this;
		}
	}
}