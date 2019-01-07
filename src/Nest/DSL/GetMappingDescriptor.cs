﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetMappingRequest : IIndexTypePath<GetMappingRequestParameters> { }
	public interface IGetMappingRequest<T> : IGetMappingRequest where T : class { }

	internal static class GetMappingPathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetMappingRequestParameters> pathInfo, IGetMappingRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class GetMappingRequest : IndexTypePathBase<GetMappingRequestParameters>, IGetMappingRequest
	{
		public GetMappingRequest(IndexNameMarker index, TypeNameMarker typeNameMarker) : base(index, typeNameMarker)
		{
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetMappingRequestParameters> pathInfo)
		{
			GetMappingPathInfo.Update(pathInfo, this);
		}
	}
	
	public partial class GetMappingRequest<T> : IndexTypePathBase<GetMappingRequestParameters, T>, IGetMappingRequest
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetMappingRequestParameters> pathInfo)
		{
			GetMappingPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesGetMapping")]
	public partial class GetMappingDescriptor<T> : IndexTypePathDescriptor<GetMappingDescriptor<T>, GetMappingRequestParameters, T>, IGetMappingRequest
		where T : class
	{
		[Obsolete("Please use the overload taking an enum", true)]
		public GetIndexDescriptor ExpandWildcards(params string[] expandWildcards)
		{
			throw new NotImplementedException();
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetMappingRequestParameters> pathInfo)
		{
			GetMappingPathInfo.Update(pathInfo, this);
		}
	}
}
