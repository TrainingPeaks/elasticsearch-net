﻿using System;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, AnalyzeDescriptor> analyzeSelector)
		{
			return this.Dispatcher.Dispatch<AnalyzeDescriptor, AnalyzeRequestParameters, AnalyzeResponse>(
				analyzeSelector,
				(p, d) =>
				{
					IRequest<AnalyzeRequestParameters> request = d;
					var text = request.RequestParameters.GetQueryStringValue<string>("text");
					request.RequestParameters.RemoveQueryString("text");
					return this.RawDispatch.IndicesAnalyzeDispatch<AnalyzeResponse>(p, text);
				}
			);
		}

		/// <inheritdoc />
		public IAnalyzeResponse Analyze(IAnalyzeRequest analyzeRequest)
		{
			return this.Dispatcher.Dispatch<IAnalyzeRequest, AnalyzeRequestParameters, AnalyzeResponse>(
				analyzeRequest,
				(p, d) =>
				{
					IRequest<AnalyzeRequestParameters> request = d;
					var text = request.RequestParameters.GetQueryStringValue<string>("text");
					request.RequestParameters.RemoveQueryString("text");
					return this.RawDispatch.IndicesAnalyzeDispatch<AnalyzeResponse>(p, text);
				}
			);
		}

		/// <inheritdoc />
		public Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, AnalyzeDescriptor> analyzeSelector)
		{
			return this.Dispatcher.DispatchAsync<AnalyzeDescriptor, AnalyzeRequestParameters, AnalyzeResponse, IAnalyzeResponse>(
				analyzeSelector,
				(p, d) =>
				{
					IRequest<AnalyzeRequestParameters> request = d;
					var text = request.RequestParameters.GetQueryStringValue<string>("text");
					request.RequestParameters.RemoveQueryString("text");
					return this.RawDispatch.IndicesAnalyzeDispatchAsync<AnalyzeResponse>(p, text);
				}
			);
		}

		/// <inheritdoc />
		public Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest analyzeRequest)
		{
			return this.Dispatcher.DispatchAsync<IAnalyzeRequest, AnalyzeRequestParameters, AnalyzeResponse, IAnalyzeResponse>(
				analyzeRequest,
				(p, d) =>
				{
					IRequest<AnalyzeRequestParameters> request = d;
					var text = request.RequestParameters.GetQueryStringValue<string>("text");
					request.RequestParameters.RemoveQueryString("text");
					return this.RawDispatch.IndicesAnalyzeDispatchAsync<AnalyzeResponse>(p, text);
				}
			);
		}

	}
}