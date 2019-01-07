﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	public partial class ElasticClient
	{
		///<inheritdoc />
		public ITermVectorResponse TermVector<T>(Func<TermvectorDescriptor<T>, TermvectorDescriptor<T>> termVectorSelector)
			where T : class
		{
			return this.Dispatcher.Dispatch<TermvectorDescriptor<T>, TermvectorRequestParameters, TermVectorResponse>(
				termVectorSelector,
				(p, d) => this.RawDispatch.TermvectorDispatch<TermVectorResponse>(p, d)
			);
		}

		///<inheritdoc />
		public ITermVectorResponse TermVector(ITermvectorRequest termvectorRequest)
		{
			return this.Dispatcher.Dispatch<ITermvectorRequest, TermvectorRequestParameters, TermVectorResponse>(
				termvectorRequest,
				(p, d) => this.RawDispatch.TermvectorDispatch<TermVectorResponse>(p, d)
			);
		}

		///<inheritdoc />
		public Task<ITermVectorResponse> TermVectorAsync<T>(Func<TermvectorDescriptor<T>, TermvectorDescriptor<T>> termVectorSelector)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<TermvectorDescriptor<T>, TermvectorRequestParameters, TermVectorResponse, ITermVectorResponse>(
				termVectorSelector,
				(p, d) => this.RawDispatch.TermvectorDispatchAsync<TermVectorResponse>(p, d)
			);
		}

		///<inheritdoc />
		public Task<ITermVectorResponse> TermVectorAsync(ITermvectorRequest termvectorRequest)
		{
			return this.Dispatcher.DispatchAsync<ITermvectorRequest , TermvectorRequestParameters, TermVectorResponse, ITermVectorResponse>(
				termvectorRequest,
				(p, d) => this.RawDispatch.TermvectorDispatchAsync<TermVectorResponse>(p, d)
			);
		}

	}
}
