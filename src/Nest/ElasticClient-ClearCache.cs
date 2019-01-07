﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IShardsOperationResponse ClearCache(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<ClearCacheDescriptor, ClearCacheRequestParameters, ShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesClearCacheDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IShardsOperationResponse ClearCache(IClearCacheRequest clearCacheRequest)
		{
			return this.Dispatcher.Dispatch<IClearCacheRequest, ClearCacheRequestParameters, ShardsOperationResponse>(
				clearCacheRequest,
				(p, d) => this.RawDispatch.IndicesClearCacheDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> ClearCacheAsync(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<ClearCacheDescriptor, ClearCacheRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesClearCacheDispatchAsync<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> ClearCacheAsync(IClearCacheRequest clearCacheRequest)
		{
			return this.Dispatcher.DispatchAsync<IClearCacheRequest, ClearCacheRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				clearCacheRequest,
				(p, d) => this.RawDispatch.IndicesClearCacheDispatchAsync<ShardsOperationResponse>(p)
			);
		}

	}
}