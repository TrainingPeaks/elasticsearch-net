using System;
using System.Collections.Generic;
using System.IO;
using ES.Net.Connection.Configuration;

namespace ES.Net.Connection.RequestState
{
	public interface ITransportRequestState
	{
		IRequestTimings InitiateRequest(RequestType requestType);
		Uri CreatePathOnCurrentNode(string path);
		IRequestConfiguration RequestConfiguration { get; }
		int Retried { get; }
		DateTime StartedOn { get; }
		bool SniffedOnConnectionFailure { get; set; }
		int? Seed { get; set; }
		Uri CurrentNode { get; set; }
		List<RequestMetrics> RequestMetrics { get; set; }
		List<Exception> SeenExceptions { get; }
		Func<IElasticsearchResponse, Stream, object> ResponseCreationOverride { get; set; }
		bool UsingPooling { get; }
	}
}