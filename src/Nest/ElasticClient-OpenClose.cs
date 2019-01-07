﻿using System;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndicesOperationResponse OpenIndex(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector)
		{
			return this.Dispatcher.Dispatch<OpenIndexDescriptor, OpenIndexRequestParameters, IndicesOperationResponse>(
				openIndexSelector,
				(p, d) => this.RawDispatch.IndicesOpenDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse OpenIndex(IOpenIndexRequest openIndexRequest)
		{
			return this.Dispatcher.Dispatch<IOpenIndexRequest, OpenIndexRequestParameters, IndicesOperationResponse>(
				openIndexRequest,
				(p, d) => this.RawDispatch.IndicesOpenDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> OpenIndexAsync(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector)
		{
			return this.Dispatcher.DispatchAsync<OpenIndexDescriptor, OpenIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				openIndexSelector,
				(p, d) => this.RawDispatch.IndicesOpenDispatchAsync<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> OpenIndexAsync(IOpenIndexRequest openIndexRequest)
		{
			return this.Dispatcher.DispatchAsync<IOpenIndexRequest, OpenIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				openIndexRequest,
				(p, d) => this.RawDispatch.IndicesOpenDispatchAsync<IndicesOperationResponse>(p)
			);
		}




		/// <inheritdoc />
		public IIndicesOperationResponse CloseIndex(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector)
		{
			return this.Dispatcher.Dispatch<CloseIndexDescriptor, CloseIndexRequestParameters, IndicesOperationResponse>(
				closeIndexSelector,
				(p, d) => this.RawDispatch.IndicesCloseDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse CloseIndex(ICloseIndexRequest closeIndexRequest)
		{
			return this.Dispatcher.Dispatch<ICloseIndexRequest, CloseIndexRequestParameters, IndicesOperationResponse>(
				closeIndexRequest,
				(p, d) => this.RawDispatch.IndicesCloseDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> CloseIndexAsync(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector)
		{
			return this.Dispatcher.DispatchAsync
				<CloseIndexDescriptor, CloseIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					closeIndexSelector,
					(p, d) => this.RawDispatch.IndicesCloseDispatchAsync<IndicesOperationResponse>(p)
				);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> CloseIndexAsync(ICloseIndexRequest closeIndexRequest)
		{
			return this.Dispatcher.DispatchAsync<ICloseIndexRequest, CloseIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					closeIndexRequest,
					(p, d) => this.RawDispatch.IndicesCloseDispatchAsync<IndicesOperationResponse>(p)
				);
		}

	}
}