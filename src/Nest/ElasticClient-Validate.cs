﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IValidateResponse Validate<T>(Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector)
			where T : class
		{
			return this.Dispatcher.Dispatch<ValidateQueryDescriptor<T>, ValidateQueryRequestParameters, ValidateResponse>(
				querySelector,
				(p, d) => this.RawDispatch.IndicesValidateQueryDispatch<ValidateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IValidateResponse Validate(IValidateQueryRequest validateQueryRequest)
		{
			return this.Dispatcher.Dispatch<IValidateQueryRequest, ValidateQueryRequestParameters, ValidateResponse>(
				validateQueryRequest,
				(p, d) => this.RawDispatch.IndicesValidateQueryDispatch<ValidateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IValidateResponse> ValidateAsync<T>(Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<ValidateQueryDescriptor<T>, ValidateQueryRequestParameters, ValidateResponse, IValidateResponse>(
				querySelector,
				(p, d) => this.RawDispatch.IndicesValidateQueryDispatchAsync<ValidateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IValidateResponse> ValidateAsync(IValidateQueryRequest validateQueryRequest)
		{
			return this.Dispatcher.DispatchAsync<IValidateQueryRequest, ValidateQueryRequestParameters, ValidateResponse, IValidateResponse>(
				validateQueryRequest,
				(p, d) => this.RawDispatch.IndicesValidateQueryDispatchAsync<ValidateResponse>(p, d)
			);
		}
	}
}