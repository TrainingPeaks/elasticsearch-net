﻿using Nest17.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MappingTransform
	{
		[JsonProperty("script")]
		public string Script { get; set; }

        [JsonProperty("script_file")]
        public string ScriptFile { get; set; }

		[JsonProperty("params")]
		public IDictionary<string, string> Parameters { get; set; }

		[JsonProperty("lang")]
		public string Language { get; set; }
	}
}
