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
		public IGlobalStatsResponse IndicesStats(Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<IndicesStatsDescriptor, IndicesStatsRequestParameters, GlobalStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesStatsDispatch<GlobalStatsResponse>(p)
			);
		}

		/// <inheritdoc />
		public IGlobalStatsResponse IndicesStats(IIndicesStatsRequest statsRequest)
		{
			return this.Dispatcher.Dispatch<IIndicesStatsRequest, IndicesStatsRequestParameters, GlobalStatsResponse>(
				statsRequest,
				(p, d) => this.RawDispatch.IndicesStatsDispatch<GlobalStatsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IGlobalStatsResponse> IndicesStatsAsync(Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<IndicesStatsDescriptor, IndicesStatsRequestParameters, GlobalStatsResponse, IGlobalStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesStatsDispatchAsync<GlobalStatsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IGlobalStatsResponse> IndicesStatsAsync(IIndicesStatsRequest statsRequest)
		{
			return this.Dispatcher.DispatchAsync<IIndicesStatsRequest, IndicesStatsRequestParameters, GlobalStatsResponse, IGlobalStatsResponse>(
				statsRequest,
				(p, d) => this.RawDispatch.IndicesStatsDispatchAsync<GlobalStatsResponse>(p)
			);
		}

	}
}