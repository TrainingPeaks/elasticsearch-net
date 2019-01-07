﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITypeExistsRequest : IIndexTypePath<TypeExistsRequestParameters> { }

	internal static class TypeExistsPathInfo
	{
		public static void Update(ElasticsearchPathInfo<TypeExistsRequestParameters> pathInfo, ITypeExistsRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.HEAD;
		}
	}
	
	public partial class TypeExistsRequest : IndexTypePathBase<TypeExistsRequestParameters>, ITypeExistsRequest
	{
		public TypeExistsRequest(IndexNameMarker index, TypeNameMarker typeNameMarker) : base(index, typeNameMarker)
		{
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TypeExistsRequestParameters> pathInfo)
		{
			TypeExistsPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesExistsType")]
	public partial class TypeExistsDescriptor : IndexTypePathDescriptor<TypeExistsDescriptor, TypeExistsRequestParameters>, ITypeExistsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TypeExistsRequestParameters> pathInfo)
		{
			TypeExistsPathInfo.Update(pathInfo, this);
		}
	}
}
