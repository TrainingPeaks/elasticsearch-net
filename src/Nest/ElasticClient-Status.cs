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
		public IStatusResponse Status(Func<IndicesStatusDescriptor, IndicesStatusDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<IndicesStatusDescriptor, IndicesStatusRequestParameters, StatusResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesStatusDispatch<StatusResponse>(p)
			);
		}

		/// <inheritdoc />
		public IStatusResponse Status(IIndicesStatusRequest statusRequest)
		{
			return this.Dispatcher.Dispatch<IIndicesStatusRequest, IndicesStatusRequestParameters, StatusResponse>(
				statusRequest,
				(p, d) => this.RawDispatch.IndicesStatusDispatch<StatusResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IStatusResponse> StatusAsync(Func<IndicesStatusDescriptor, IndicesStatusDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<IndicesStatusDescriptor, IndicesStatusRequestParameters, StatusResponse, IStatusResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesStatusDispatchAsync<StatusResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IStatusResponse> StatusAsync(IIndicesStatusRequest statusRequest)
		{
			return this.Dispatcher.DispatchAsync<IIndicesStatusRequest, IndicesStatusRequestParameters, StatusResponse, IStatusResponse>(
				statusRequest,
				(p, d) => this.RawDispatch.IndicesStatusDispatchAsync<StatusResponse>(p)
			);
		}

	}
}