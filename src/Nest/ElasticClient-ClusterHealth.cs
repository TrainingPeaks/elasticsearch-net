﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector = null)
		{
			clusterHealthSelector = clusterHealthSelector ?? (s => s);
			return this.Dispatcher.Dispatch<ClusterHealthDescriptor, ClusterHealthRequestParameters, HealthResponse>(
				clusterHealthSelector,
				(p, d) => this.RawDispatch.ClusterHealthDispatch<HealthResponse>(p)
			);
		}

		/// <inheritdoc />
		public IHealthResponse ClusterHealth(IClusterHealthRequest clusterHealthRequest)
		{
			return this.Dispatcher.Dispatch<IClusterHealthRequest, ClusterHealthRequestParameters, HealthResponse>(
				clusterHealthRequest,
				(p, d) => this.RawDispatch.ClusterHealthDispatch<HealthResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector = null)
		{
			clusterHealthSelector = clusterHealthSelector ?? (s => s);
			return this.Dispatcher.DispatchAsync<ClusterHealthDescriptor, ClusterHealthRequestParameters, HealthResponse, IHealthResponse>(
				clusterHealthSelector,
				(p, d) => this.RawDispatch.ClusterHealthDispatchAsync<HealthResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IHealthResponse> ClusterHealthAsync(IClusterHealthRequest clusterHealthRequest)
		{
			return this.Dispatcher.DispatchAsync<IClusterHealthRequest, ClusterHealthRequestParameters, HealthResponse, IHealthResponse>(
				clusterHealthRequest,
				(p, d) => this.RawDispatch.ClusterHealthDispatchAsync<HealthResponse>(p)
			);
		}

	}
}