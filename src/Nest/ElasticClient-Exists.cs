﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	using ExistConverter = Func<IElasticsearchResponse, Stream, ExistsResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExistsResponse DocumentExists<T>(Func<DocumentExistsDescriptor<T>, DocumentExistsDescriptor<T>> existsSelector)
			where T : class
		{
			return this.Dispatcher.Dispatch<DocumentExistsDescriptor<T>, DocumentExistsRequestParameters, ExistsResponse>(
				d => existsSelector(d.RequestConfiguration(r=>r.AllowedStatusCodes(404))),
				(p, d) => this.RawDispatch.ExistsDispatch<ExistsResponse>(p
					.DeserializationState(new ExistConverter(this.DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public IExistsResponse DocumentExists(IDocumentExistsRequest documentExistsRequest)
		{
			return this.Dispatcher.Dispatch<IDocumentExistsRequest, DocumentExistsRequestParameters, ExistsResponse>(
				documentExistsRequest,
				(p, d) => this.RawDispatch.ExistsDispatch<ExistsResponse>(p
					.DeserializationState(new ExistConverter(this.DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> DocumentExistsAsync<T>(Func<DocumentExistsDescriptor<T>, DocumentExistsDescriptor<T>> existsSelector)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<DocumentExistsDescriptor<T>, DocumentExistsRequestParameters, ExistsResponse, IExistsResponse>(
				d => existsSelector(d.RequestConfiguration(r=>r.AllowedStatusCodes(404))),
				(p, d) => this.RawDispatch.ExistsDispatchAsync<ExistsResponse>(p
					.DeserializationState(new ExistConverter(this.DeserializeExistsResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest documentExistsRequest)
		{
			return this.Dispatcher.DispatchAsync<IDocumentExistsRequest, DocumentExistsRequestParameters, ExistsResponse, IExistsResponse>(
				documentExistsRequest,
				(p, d) => this.RawDispatch.ExistsDispatchAsync<ExistsResponse>(p
					.DeserializationState(new ExistConverter(this.DeserializeExistsResponse))
				)
			);
		}
	}
}