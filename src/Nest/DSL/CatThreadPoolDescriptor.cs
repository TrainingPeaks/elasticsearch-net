using ES.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatThreadPoolRequest : IRequest<CatThreadPoolRequestParameters>
	{
	}

	public partial class CatThreadPoolRequest : BasePathRequest<CatThreadPoolRequestParameters>, ICatThreadPoolRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatThreadPoolRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class CatThreadPoolDescriptor : BasePathDescriptor<CatThreadPoolDescriptor, CatThreadPoolRequestParameters>, ICatThreadPoolRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatThreadPoolRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}
}
