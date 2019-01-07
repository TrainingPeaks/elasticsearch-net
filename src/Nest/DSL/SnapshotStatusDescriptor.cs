﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISnapshotStatusRequest : IRepositorySnapshotOptionalPath<SnapshotStatusRequestParameters> { }

	internal static class SnapshotStatusPathInfo
	{
		public static void Update(ElasticsearchPathInfo<SnapshotStatusRequestParameters> pathInfo, ISnapshotStatusRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class SnapshotStatusRequest : RepositorySnapshotOptionalPathBase<SnapshotStatusRequestParameters>, ISnapshotStatusRequest
	{
		public SnapshotStatusRequest() : base() {}
		public SnapshotStatusRequest(string repository, params string[] snapshots) : base(repository, snapshots) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SnapshotStatusRequestParameters> pathInfo)
		{
			SnapshotStatusPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("SnapshotGet")]
	public partial class SnapshotStatusDescriptor : RepositorySnapshotOptionalPathDescriptor<SnapshotStatusDescriptor, SnapshotStatusRequestParameters>, ISnapshotStatusRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SnapshotStatusRequestParameters> pathInfo)
		{
			SnapshotStatusPathInfo.Update(pathInfo, this);
		}

	}
}
