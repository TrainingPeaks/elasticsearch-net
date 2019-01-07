﻿using System;
using System.IO;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	using SearchExistConverter = Func<IElasticsearchResponse, Stream, ExistsResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse SearchExists<T>(Func<SearchExistsDescriptor<T>, SearchExistsDescriptor<T>> selector)
			where T : class
		{
			return this.Dispatcher.Dispatch<SearchExistsDescriptor<T>, SearchExistsRequestParameters, ExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.SearchExistsDispatch<ExistsResponse>(
					p.DeserializationState(new SearchExistConverter(DeserializeExistsResponse))
					, d
					)
				);
		}

		/// <inheritdoc />
		public IExistsResponse SearchExists(ISearchExistsRequest indexRequest)
		{
			return this.Dispatcher.Dispatch<ISearchExistsRequest, SearchExistsRequestParameters, ExistsResponse>(
				indexRequest,
				(p, d) => this.RawDispatch.SearchExistsDispatch<ExistsResponse>(
					p.DeserializationState(new SearchExistConverter(DeserializeExistsResponse))
					, d
					)
				);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> SearchExistsAsync<T>(Func<SearchExistsDescriptor<T>, SearchExistsDescriptor<T>> selector)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<SearchExistsDescriptor<T>, SearchExistsRequestParameters, ExistsResponse, IExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.SearchExistsDispatchAsync<ExistsResponse>(
					p.DeserializationState(new SearchExistConverter(DeserializeExistsResponse))
					, d
					)
				);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> SearchExistsAsync(ISearchExistsRequest indexRequest)
		{
			return this.Dispatcher.DispatchAsync<ISearchExistsRequest, SearchExistsRequestParameters, ExistsResponse, IExistsResponse>(
				indexRequest,
				(p, d) => this.RawDispatch.SearchExistsDispatchAsync<ExistsResponse>(
					p.DeserializationState(new SearchExistConverter(DeserializeExistsResponse))
					, d
					)
				);
		}


	}
}