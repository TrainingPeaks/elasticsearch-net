﻿using System.Collections.Generic;
using ES.Net;
using Newtonsoft.Json;

namespace Nest17
{
	public interface IPercolateCountResponse : IResponse
	{
		int Took { get; }
		long Total { get; }
	}


	public interface IPercolateResponse : IPercolateCountResponse
	{
		IEnumerable<PercolatorMatch> Matches { get; }
	}

	[JsonObject]
	public class PercolateCountResponse : BaseResponse, IPercolateCountResponse
	{
		public PercolateCountResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "took")]
		public int Took { get; internal set; }
		[JsonProperty(PropertyName = "total")]
		public long Total { get; internal set; }
		
		/// <summary>
		/// The individual error for separate requests on the _mpercolate API
		/// </summary>
		[JsonProperty(PropertyName = "error")]
		internal string Error { get; set; }

		public override ElasticsearchServerError ServerError
		{
			get
			{
				if (this.Error.IsNullOrEmpty()) return base.ServerError;
				return new ElasticsearchServerError
				{
					Error = this.Error,
					Status = 500
				};
			}
		}
	}

	[JsonObject]
	public class PercolateResponse : PercolateCountResponse, IPercolateResponse
	{

		[JsonProperty(PropertyName = "matches")]
		public IEnumerable<PercolatorMatch> Matches { get; internal set; }
	}

	public class PercolatorMatch
	{
		[JsonProperty(PropertyName = "_index")]
		public string Index { get; set; }
		[JsonProperty(PropertyName = "_id")]
		public string Id { get; set; }
		[JsonProperty(PropertyName = "highlight")]
        public Dictionary<string, IList<string>> Highlight { get; set; }
        [JsonProperty(PropertyName = "_score")]
        public double Score { get; set; }
		
	}

}