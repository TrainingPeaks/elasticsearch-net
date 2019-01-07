﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ES.Net;
using Newtonsoft.Json;

namespace Nest17
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISearchShardsResponse SearchShards<T>(Func<SearchShardsDescriptor<T>, SearchShardsDescriptor<T>> searchSelector) where T : class
		{
			return this.Dispatcher.Dispatch<SearchShardsDescriptor<T>, SearchShardsRequestParameters, SearchShardsResponse>(
				searchSelector,
				(p, d) => this.RawDispatch.SearchShardsDispatch<SearchShardsResponse>(p)
			);
		}
		
		/// <inheritdoc />
		public ISearchShardsResponse SearchShards(ISearchShardsRequest request)
		{
			return this.Dispatcher.Dispatch<ISearchShardsRequest, SearchShardsRequestParameters, SearchShardsResponse>(
				request,
				(p, d) => this.RawDispatch.SearchShardsDispatch<SearchShardsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, SearchShardsDescriptor<T>> searchSelector)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<SearchShardsDescriptor<T>, SearchShardsRequestParameters, SearchShardsResponse, ISearchShardsResponse>(
				searchSelector,
				(p, d) => this.RawDispatch.SearchShardsDispatchAsync<SearchShardsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request)
		{
			return this.Dispatcher.DispatchAsync<ISearchShardsRequest, SearchShardsRequestParameters, SearchShardsResponse, ISearchShardsResponse>(
				request,
				(p, d) => this.RawDispatch.SearchShardsDispatchAsync<SearchShardsResponse>(p)
			);
		}
		
	}
}