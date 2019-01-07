﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest17.DSL.Query.Behaviour;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IWildcardQuery : ITermQuery
	{
		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }
	}

	public class WildcardQuery<T> : WildcardQuery
		where T : class
	{
		public WildcardQuery(Expression<Func<T, object>> field)
		{
			this.Field = field;
		}
	}
	public class WildcardQuery : PlainQuery, IWildcardQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		PropertyPathMarker IFieldNameQuery.GetFieldName() { return this.Field; }

		void IFieldNameQuery.SetFieldName(string fieldName) { this.Field = fieldName; }

		public PropertyPathMarker Field { get; set; }
		public object Value { get; set; }
		public double? Boost { get; set; }
		public RewriteMultiTerm? Rewrite { get; set; }


		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Wildcard = this;
		}
	}

	public class WildcardQueryDescriptor<T> : 
		TermQueryDescriptorBase<WildcardQueryDescriptor<T>, T>, 
		IWildcardQuery 
		where T : class
	{
		RewriteMultiTerm? IWildcardQuery.Rewrite { get; set; }

		public WildcardQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IWildcardQuery)this).Rewrite = rewrite;
			return this;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IWildcardQuery)this).Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			((IWildcardQuery)this).Field = fieldName;
		}
	}
}
