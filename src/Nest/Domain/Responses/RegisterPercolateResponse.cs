﻿using Newtonsoft.Json;

namespace Nest17
{
	public interface IRegisterPercolateResponse : IResponse
	{
		bool Created { get; }
		string Index { get; }
		string Type { get; }
		string Id { get; }
		int Version { get; }
	}

	[JsonObject]
	public class RegisterPercolateResponse : BaseResponse, IRegisterPercolateResponse
	{
		public RegisterPercolateResponse()
		{
			this.IsValid = true;
		}
		[JsonProperty(PropertyName = "created")]
		public bool Created { get; internal set; }

		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }

		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }

		[JsonProperty(PropertyName = "_id")]
		public string Id { get; internal set; }

		[JsonProperty(PropertyName = "_version")]
		public int Version { get; internal set; }
	}
}