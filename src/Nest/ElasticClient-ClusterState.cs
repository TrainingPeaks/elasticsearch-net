﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterStateResponse ClusterState(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null)
		{
			clusterStateSelector = clusterStateSelector ?? (s => s);
			return this.Dispatcher.Dispatch<ClusterStateDescriptor, ClusterStateRequestParameters, ClusterStateResponse>(
				clusterStateSelector,
				(p, d) => this.RawDispatch.ClusterStateDispatch<ClusterStateResponse>(p)
			);
		}

		/// <inheritdoc />
		public IClusterStateResponse ClusterState(IClusterStateRequest clusterStateRequest)
		{
			return this.Dispatcher.Dispatch<IClusterStateRequest, ClusterStateRequestParameters, ClusterStateResponse>(
				clusterStateRequest,
				(p, d) => this.RawDispatch.ClusterStateDispatch<ClusterStateResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null)
		{
			clusterStateSelector = clusterStateSelector ?? (s => s);
			return this.Dispatcher.DispatchAsync<ClusterStateDescriptor, ClusterStateRequestParameters, ClusterStateResponse, IClusterStateResponse>(
				clusterStateSelector,
				(p, d) => this.RawDispatch.ClusterStateDispatchAsync<ClusterStateResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IClusterStateResponse> ClusterStateAsync(IClusterStateRequest clusterStateRequest)
		{
			return this.Dispatcher.DispatchAsync<IClusterStateRequest, ClusterStateRequestParameters, ClusterStateResponse, IClusterStateResponse>(
				clusterStateRequest,
				(p, d) => this.RawDispatch.ClusterStateDispatchAsync<ClusterStateResponse>(p)
			);
		}


	}
}