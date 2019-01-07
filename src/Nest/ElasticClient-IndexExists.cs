﻿using System;
using System.IO;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	using IndexExistConverter = Func<IElasticsearchResponse, Stream, ExistsResponse>;

	public partial class ElasticClient
	{

		/// <inheritdoc />
		public IExistsResponse IndexExists(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.Dispatcher.Dispatch<IndexExistsDescriptor, IndexExistsRequestParameters, ExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsDispatch<ExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public IExistsResponse IndexExists(IIndexExistsRequest indexRequest)
		{
			return this.Dispatcher.Dispatch<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse>(
				indexRequest,
				(p, d) => this.RawDispatch.IndicesExistsDispatch<ExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> IndexExistsAsync(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector)
		{
			return this.Dispatcher.DispatchAsync<IndexExistsDescriptor, IndexExistsRequestParameters, ExistsResponse, IExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsDispatchAsync<ExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest indexRequest)
		{
			return this.Dispatcher.DispatchAsync<IIndexExistsRequest, IndexExistsRequestParameters, ExistsResponse, IExistsResponse>(
				indexRequest,
				(p, d) => this.RawDispatch.IndicesExistsDispatchAsync<ExistsResponse>(
					p.DeserializationState(new IndexExistConverter(DeserializeExistsResponse))
				)
			);
		}


		private ExistsResponse DeserializeExistsResponse(IElasticsearchResponse response, Stream stream)
		{
			return new ExistsResponse(response);
		}
	}
}