﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using Newtonsoft.Json;
using Nest17.Resolvers.Converters;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteByQueryRequest : IQueryPath<DeleteByQueryRequestParameters>
	{
		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }
	}

	public interface IDeleteByQueryRequest<T> : IDeleteByQueryRequest where T : class {}

	internal static class DeleteByQueryPathInfo
	{
		public static void Update(ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo, IDeleteByQueryRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
			//query works a bit different in that if all types and all indices are specified the root 
			//needs to be /_all/_query not just /_query
			if (pathInfo.Index.IsNullOrEmpty() && pathInfo.Type.IsNullOrEmpty()
				&& request.AllIndices.GetValueOrDefault(false)
				&& request.AllTypes.GetValueOrDefault(false))
				pathInfo.Index = "_all";

		}
	}
	

	public partial class DeleteByQueryRequest : QueryPathBase<DeleteByQueryRequestParameters>, IDeleteByQueryRequest
	{
		public DeleteByQueryRequest() {}

		public DeleteByQueryRequest(IndexNameMarker index, TypeNameMarker type = null) : base(index, type) { }

		public DeleteByQueryRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null) : base(indices, types) { }

		public IQueryContainer Query { get; set; }


		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}

	}

	public partial class DeleteByQueryRequest<T> : QueryPathBase<DeleteByQueryRequestParameters, T>, IDeleteByQueryRequest where T : class
	{
		public DeleteByQueryRequest() {}

		public DeleteByQueryRequest(IndexNameMarker index, TypeNameMarker type = null) : base(index, type) { }

		public DeleteByQueryRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null) : base(indices, types) { }

		public IQueryContainer Query { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}

	}

	public partial class DeleteByQueryDescriptor<T> 
		: QueryPathDescriptorBase<DeleteByQueryDescriptor<T>, DeleteByQueryRequestParameters, T>, IDeleteByQueryRequest
		where T : class
	{
		private IDeleteByQueryRequest Self { get { return this; } }

		IQueryContainer IDeleteByQueryRequest.Query { get; set; }

		public DeleteByQueryDescriptor<T> MatchAll()
		{
			Self.Query = new QueryDescriptor<T>().MatchAll();
			return this;
		}

		public DeleteByQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo)
		{
			DeleteByQueryPathInfo.Update(pathInfo, this);
		}
	}
}
