﻿using ES.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest17
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterPendingTasksResponse ClusterPendingTasks(Func<ClusterPendingTasksDescriptor, ClusterPendingTasksDescriptor> pendingTasksSelector = null)
		{
			pendingTasksSelector = pendingTasksSelector ?? (s => s);
			return this.Dispatcher.Dispatch<ClusterPendingTasksDescriptor, ClusterPendingTasksRequestParameters, ClusterPendingTasksResponse>(
				pendingTasksSelector,
				(p, d) => this.RawDispatch.ClusterPendingTasksDispatch<ClusterPendingTasksResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(Func<ClusterPendingTasksDescriptor, ClusterPendingTasksDescriptor> pendingTasksSelector = null)
		{
			pendingTasksSelector = pendingTasksSelector ?? (s => s);
			return this.Dispatcher.DispatchAsync<ClusterPendingTasksDescriptor, ClusterPendingTasksRequestParameters, ClusterPendingTasksResponse, IClusterPendingTasksResponse>(
				pendingTasksSelector,
				(p, d) => this.RawDispatch.ClusterPendingTasksDispatchAsync<ClusterPendingTasksResponse>(p)
			);
		}

		/// <inheritdoc />
		public IClusterPendingTasksResponse ClusterPendingTasks(IClusterPendingTasksRequest pendingTasksRequest)
		{
			return this.Dispatcher.Dispatch<IClusterPendingTasksRequest, ClusterPendingTasksRequestParameters, ClusterPendingTasksResponse>(
				pendingTasksRequest,
				(p, d) => this.RawDispatch.ClusterPendingTasksDispatch<ClusterPendingTasksResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest pendingTasksRequest)
		{
			return this.Dispatcher.DispatchAsync<IClusterPendingTasksRequest, ClusterPendingTasksRequestParameters, ClusterPendingTasksResponse, IClusterPendingTasksResponse>(
				pendingTasksRequest,
				(p, d) => this.RawDispatch.ClusterPendingTasksDispatchAsync<ClusterPendingTasksResponse>(p)
			);
		}
	}
}
