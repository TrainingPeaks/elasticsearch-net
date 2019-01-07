using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IInfoRequest : IRequest<InfoRequestParameters> { }

	internal static class InfoPathInfo
	{
		public static void Update(ElasticsearchPathInfo<InfoRequestParameters> pathInfo, IInfoRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class InfoRequest : BasePathRequest<InfoRequestParameters>, IInfoRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<InfoRequestParameters> pathInfo)
		{
			InfoPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("Info")]
	public partial class InfoDescriptor : BasePathDescriptor<InfoDescriptor, InfoRequestParameters>, IInfoRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<InfoRequestParameters> pathInfo)
		{
			InfoPathInfo.Update(pathInfo, this);
		}
	}
}
