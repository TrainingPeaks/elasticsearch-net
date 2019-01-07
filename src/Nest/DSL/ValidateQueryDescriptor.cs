﻿using System;
using System.Collections.Generic;
using ES.Net;
using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IValidateQueryRequest : IQueryPath<ValidateQueryRequestParameters>
    {
        [JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
        IQueryContainer Query { get; set; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IValidateQueryRequest<T> : IValidateQueryRequest
        where T : class { }

    internal static class ValidateQueryPathInfo
    {
        public static void Update(ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo, IValidateQueryRequest request)
        {
            var source = request.RequestParameters.GetQueryStringValue<string>("source");
            var q = request.RequestParameters.GetQueryStringValue<string>("q");
            pathInfo.HttpMethod = (!source.IsNullOrEmpty() || !q.IsNullOrEmpty())
                ? PathInfoHttpMethod.GET
                : PathInfoHttpMethod.POST;
        }
    }

    public partial class ValidateQueryRequest : QueryPathBase<ValidateQueryRequestParameters>, IValidateQueryRequest
    {
		public ValidateQueryRequest() {}

	    public ValidateQueryRequest(IndexNameMarker index, TypeNameMarker type = null) : base(index, type) { }

	    public ValidateQueryRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null) : base(indices, types) { }

	    public IQueryContainer Query { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo)
        {
            ValidateQueryPathInfo.Update(pathInfo, this);
        }
    }

    public partial class ValidateQueryRequest<T> : QueryPathBase<ValidateQueryRequestParameters, T>, IValidateQueryRequest<T>
        where T : class
    {

		public ValidateQueryRequest() {}

	    public ValidateQueryRequest(IndexNameMarker index, TypeNameMarker type = null) : base(index, type) { }

	    public ValidateQueryRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null) : base(indices, types) { }

	    public IQueryContainer Query { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo)
        {
            ValidateQueryPathInfo.Update(pathInfo, this);
        }
    }

	[DescriptorFor("IndicesValidateQuery")]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public partial class ValidateQueryDescriptor<T> 
        : QueryPathDescriptorBase<ValidateQueryDescriptor<T>, ValidateQueryRequestParameters, T>, IValidateQueryRequest<T>
		where T : class
	{
        private IValidateQueryRequest Self { get { return this; } }

        IQueryContainer IValidateQueryRequest.Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo)
		{
            ValidateQueryPathInfo.Update(pathInfo, this);
		}
	}
}
