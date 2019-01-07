﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISegmentsResponse Segments(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector = null)
		{
			segmentsSelector = segmentsSelector ?? (s => s);
			return this.Dispatcher.Dispatch<SegmentsDescriptor, SegmentsRequestParameters, SegmentsResponse>(
				segmentsSelector,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatch<SegmentsResponse>(p)
			);
		}

		/// <inheritdoc />
		public ISegmentsResponse Segments(ISegmentsRequest segmentsRequest)
		{
			return this.Dispatcher.Dispatch<ISegmentsRequest, SegmentsRequestParameters, SegmentsResponse>(
				segmentsRequest,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatch<SegmentsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISegmentsResponse> SegmentsAsync(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector = null)
		{
			segmentsSelector = segmentsSelector ?? (s => s);
			return this.Dispatcher.DispatchAsync<SegmentsDescriptor, SegmentsRequestParameters, SegmentsResponse, ISegmentsResponse>(
				segmentsSelector,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatchAsync<SegmentsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISegmentsResponse> SegmentsAsync(ISegmentsRequest segmentsRequest)
		{
			return this.Dispatcher.DispatchAsync<ISegmentsRequest, SegmentsRequestParameters, SegmentsResponse, ISegmentsResponse>(
				segmentsRequest,
				(p, d) => this.RawDispatch.IndicesSegmentsDispatchAsync<SegmentsResponse>(p)
			);
		}

	}
}