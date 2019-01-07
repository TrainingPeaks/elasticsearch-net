﻿using ES.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICatIndicesRequest : IRequest<CatIndicesRequestParameters>
	{
	}

	public partial class CatIndicesRequest : BasePathRequest<CatIndicesRequestParameters>, ICatIndicesRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatIndicesRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class CatIndicesDescriptor : BasePathDescriptor<CatIndicesDescriptor, CatIndicesRequestParameters>, ICatIndicesRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CatIndicesRequestParameters> pathInfo)
		{
			CatRequestPathInfo.Update(pathInfo);
		}
	}
}
