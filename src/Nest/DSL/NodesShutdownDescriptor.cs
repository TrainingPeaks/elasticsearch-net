﻿using ES.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	internal static class NodesShutdownPathInfo
	{
		public static void Update(ElasticsearchPathInfo<NodesShutdownRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface INodesShutdownRequest : INodeIdOptionalPath<NodesShutdownRequestParameters>
	{
	}

	public partial class NodesShutdownRequest 
		: NodeIdOptionalPathBase<NodesShutdownRequestParameters>, INodesShutdownRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<NodesShutdownRequestParameters> pathInfo)
		{
			NodesShutdownPathInfo.Update(pathInfo);
		}
	}

	public partial class NodesShutdownDescriptor 
		: NodeIdOptionalDescriptor<NodesShutdownDescriptor, NodesShutdownRequestParameters>, INodesShutdownRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<NodesShutdownRequestParameters> pathInfo)
		{
			NodesShutdownPathInfo.Update(pathInfo);
		}
	}
}
