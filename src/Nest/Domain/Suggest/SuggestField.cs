﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	[JsonObject]
	public class SuggestField
	{
		[JsonProperty("input")]
		public string Input { get; set; }

		[JsonProperty("output")]
		public string Output { get; set; }

		[JsonProperty("payload")]
		public object Payload { get; set; }

		[JsonProperty("weight")]
		public uint Weight { get; set; }

		[JsonProperty("context")]
		public IDictionary<string, IEnumerable<string>> Context { get; set; }
	}
}
