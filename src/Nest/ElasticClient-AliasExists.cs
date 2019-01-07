﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	using AliasExistConverter = Func<IElasticsearchResponse, Stream, ExistsResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse AliasExists(Func<AliasExistsDescriptor, AliasExistsDescriptor> selector)
		{
			return this.Dispatcher.Dispatch<AliasExistsDescriptor, AliasExistsRequestParameters, ExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsAliasDispatch<ExistsResponse>(
					p.DeserializationState(new AliasExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public IExistsResponse AliasExists(IAliasExistsRequest AliasRequest)
		{
			return this.Dispatcher.Dispatch<IAliasExistsRequest, AliasExistsRequestParameters, ExistsResponse>(
				AliasRequest,
				(p, d) => this.RawDispatch.IndicesExistsAliasDispatch<ExistsResponse>(
					p.DeserializationState(new AliasExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> AliasExistsAsync(Func<AliasExistsDescriptor, AliasExistsDescriptor> selector)
		{
			return this.Dispatcher.DispatchAsync<AliasExistsDescriptor, AliasExistsRequestParameters, ExistsResponse, IExistsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesExistsAliasDispatchAsync<ExistsResponse>(
					p.DeserializationState(new AliasExistConverter(DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> AliasExistsAsync(IAliasExistsRequest AliasRequest)
		{
			return this.Dispatcher.DispatchAsync<IAliasExistsRequest, AliasExistsRequestParameters, ExistsResponse, IExistsResponse>(
				AliasRequest,
				(p, d) => this.RawDispatch.IndicesExistsAliasDispatchAsync<ExistsResponse>(
					p.DeserializationState(new AliasExistConverter(DeserializeExistsResponse))
				)
			);
		}

	}
}