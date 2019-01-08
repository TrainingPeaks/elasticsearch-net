﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest17
{
	public interface IClusterPutSettingsResponse : IResponse
	{
		[JsonProperty(PropertyName = "acknowledged")]
		bool Acknowledged { get; }

		[JsonProperty(PropertyName = "persistent")]
		IDictionary<string, object> Persistent { get; set; }

		[JsonProperty(PropertyName = "transient")]
		IDictionary<string, object> Transient { get; set; }
	}

	public class ClusterPutSettingsResponse : BaseResponse, IClusterPutSettingsResponse
	{
		public bool Acknowledged { get; internal set; }
		public IDictionary<string, object> Persistent { get; set; }
		public IDictionary<string, object> Transient { get; set; }
	}
}