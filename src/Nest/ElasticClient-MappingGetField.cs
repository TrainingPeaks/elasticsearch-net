﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	using GetFieldMappingConverter = Func<IElasticsearchResponse, Stream, GetFieldMappingResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetFieldMappingResponse GetFieldMapping<T>(Func<GetFieldMappingDescriptor<T>, GetFieldMappingDescriptor<T>> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<GetFieldMappingDescriptor<T>, GetFieldMappingRequestParameters, GetFieldMappingResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetFieldMappingDispatch<GetFieldMappingResponse>(
					p.DeserializationState(new GetFieldMappingConverter((r, s) => DeserializeGetFieldMappingResponse(r, d, s)))
				)
			);
		}

		/// <inheritdoc />
		public IGetFieldMappingResponse GetFieldMapping(IGetFieldMappingRequest getFieldMappingRequest)
		{
			return this.Dispatcher.Dispatch<IGetFieldMappingRequest, GetFieldMappingRequestParameters, GetFieldMappingResponse>(
				getFieldMappingRequest,
				(p, d) => this.RawDispatch.IndicesGetFieldMappingDispatch<GetFieldMappingResponse>(
					p.DeserializationState(new GetFieldMappingConverter((r, s) => DeserializeGetFieldMappingResponse(r, d, s)))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Func<GetFieldMappingDescriptor<T>, GetFieldMappingDescriptor<T>> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<GetFieldMappingDescriptor<T>, GetFieldMappingRequestParameters, GetFieldMappingResponse, IGetFieldMappingResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetFieldMappingDispatchAsync<GetFieldMappingResponse>(
					p.DeserializationState(new GetFieldMappingConverter((r, s) => DeserializeGetFieldMappingResponse(r, d, s)))
				)
			);
		}

		/// <inheritdoc />
		public Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest getFieldMappingRequest)
		{
			return this.Dispatcher.DispatchAsync<IGetFieldMappingRequest, GetFieldMappingRequestParameters, GetFieldMappingResponse, IGetFieldMappingResponse>(
				getFieldMappingRequest,
				(p, d) => this.RawDispatch.IndicesGetFieldMappingDispatchAsync<GetFieldMappingResponse>(
					p.DeserializationState(new GetFieldMappingConverter((r, s) => DeserializeGetFieldMappingResponse(r, d, s)))
				)
			);
		}

		private GetFieldMappingResponse DeserializeGetFieldMappingResponse(IElasticsearchResponse response, IGetFieldMappingRequest d, Stream stream)
		{
			var dict = response.Success
				? Serializer.Deserialize<IndexFieldMappings>(stream)
				: null;
			return new GetFieldMappingResponse(response, dict);
		}

	}
}