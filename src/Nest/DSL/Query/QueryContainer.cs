﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nest17.DSL.Visitor;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization.OptIn)]
	public class QueryContainer : IQueryContainer
	{
		private IQueryContainer Self { get { return this; } }

		IBoolQuery IQueryContainer.Bool { get; set; }

		string IQueryContainer.RawQuery { get; set; }

		IMatchAllQuery IQueryContainer.MatchAllQuery { get; set; }
		
		ITermQuery IQueryContainer.Term { get; set; }
		
		IWildcardQuery IQueryContainer.Wildcard { get; set; }
		
		IPrefixQuery IQueryContainer.Prefix { get; set; }

		IBoostingQuery IQueryContainer.Boosting { get; set; }
		
		IIdsQuery IQueryContainer.Ids { get; set; }
		
		ICustomScoreQuery IQueryContainer.CustomScore { get; set; }
		
		ICustomFiltersScoreQuery IQueryContainer.CustomFiltersScore { get; set; }

		ICustomBoostFactorQuery IQueryContainer.CustomBoostFactor { get; set; }
		
		IConstantScoreQuery IQueryContainer.ConstantScore { get; set; }
		
		IDisMaxQuery IQueryContainer.DisMax { get; set; }
		
		IFilteredQuery IQueryContainer.Filtered { get; set; }

		IMultiMatchQuery IQueryContainer.MultiMatch { get; set; }
		
		IMatchQuery IQueryContainer.Match { get; set; }
		
		IFuzzyQuery IQueryContainer.Fuzzy { get; set; }
		
		IGeoShapeQuery IQueryContainer.GeoShape { get; set; }
		
		ICommonTermsQuery IQueryContainer.CommonTerms { get; set; }
		
		ITermsQuery IQueryContainer.Terms { get; set; }
		
		IQueryStringQuery IQueryContainer.QueryString { get; set; }
		
		ISimpleQueryStringQuery IQueryContainer.SimpleQueryString { get; set; }
		
		IRegexpQuery IQueryContainer.Regexp { get; set; }

		IFuzzyLikeThisQuery IQueryContainer.FuzzyLikeThis { get; set; }
		
		IHasChildQuery IQueryContainer.HasChild { get; set; }
		
		IHasParentQuery IQueryContainer.HasParent { get; set; }
		
		IMoreLikeThisQuery IQueryContainer.MoreLikeThis { get; set; }
		
		IRangeQuery IQueryContainer.Range { get; set; }

		ISpanTermQuery IQueryContainer.SpanTerm { get; set; }
		
		ISpanFirstQuery IQueryContainer.SpanFirst { get; set; }
		
		ISpanOrQuery IQueryContainer.SpanOr { get; set; }
		
		ISpanNotQuery IQueryContainer.SpanNot { get; set; }
		
		ISpanNearQuery IQueryContainer.SpanNear { get; set; }

		ISpanMultiTermQuery IQueryContainer.SpanMultiTerm { get; set; }

		ITopChildrenQuery IQueryContainer.TopChildren { get; set; }

		INestedQuery IQueryContainer.Nested { get; set; }
		
		IIndicesQuery IQueryContainer.Indices { get; set; }

		IFunctionScoreQuery IQueryContainer.FunctionScore { get; set; }

		ITemplateQuery IQueryContainer.Template { get; set; }
		
		bool IQueryContainer.IsConditionless { get; set; }
		public bool IsConditionless { get { return Self.IsConditionless; } }

		bool IQueryContainer.IsStrict { get; set; }
		public bool IsStrict { get { return Self.IsStrict; } }
		
		bool IQueryContainer.IsVerbatim { get; set; }
		public bool IsVerbatim { get { return Self.IsVerbatim; } }

		public QueryContainer() {}
	
		public QueryContainer(PlainQuery query)
		{
			PlainQuery.ToContainer(query, this);
		}
	
		public static QueryContainer From(PlainQuery query)
		{
			return PlainQuery.ToContainer(query);
		}

		public static QueryContainer operator &(QueryContainer leftContainer, QueryContainer rightContainer)
		{
			QueryContainer queryContainer;
			if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out queryContainer)) return queryContainer;

			return leftContainer.MergeMustQueries(rightContainer);
		}
		
		public static QueryContainer operator |(QueryContainer leftContainer, QueryContainer rightContainer)
		{
			QueryContainer queryContainer;
			if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out queryContainer)) return queryContainer;

			return leftContainer.MergeShouldQueries(rightContainer);
		}

		private static bool IfEitherIsEmptyReturnTheOtherOrEmpty(QueryContainer leftContainer, QueryContainer rightContainer,
			out QueryContainer queryContainer)
		{
			var combined = new[] {leftContainer, rightContainer};
			queryContainer = !combined.Any(bf => bf == null || bf.IsConditionless)
				? null
				: combined.FirstOrDefault(bf => bf != null && !bf.IsConditionless) ?? CreateEmptyContainer();
			return queryContainer != null;
		}

		public static QueryContainer operator !(QueryContainer queryContainer)
		{
			if (queryContainer == null || queryContainer.IsConditionless) return CreateEmptyContainer();

			IQueryContainer f = new QueryContainer();
			f.Bool = new BoolBaseQueryDescriptor();
			f.Bool.MustNot = new[] { queryContainer };
			return f as QueryContainer;
		}

		public static bool operator false(QueryContainer a)
		{
			return false;
		}

		public static bool operator true(QueryContainer a)
		{
			return false;
		}

		public void Accept(IQueryVisitor visitor)
		{
			var walker = new QueryFilterWalker();
			if (visitor.Scope == VisitorScope.Unknown) visitor.Scope = VisitorScope.Query;
			walker.Walk(this, visitor);
		}

		private static QueryContainer CreateEmptyContainer()
		{
			var q = new QueryContainer();
			((IQueryContainer)q).IsConditionless = true;
			return q;
		}

		public object GetCustomJson()
		{	
			var f = ((IQueryContainer)this);
			if (f.RawQuery.IsNullOrEmpty()) return f; 
			return new RawJson(f.RawQuery);
		}
	}
}
