﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	[JsonObject]
	public interface ISuggestContext
	{
		[JsonProperty("type")]
		string Type { get; }

		[JsonProperty("path")]
		PropertyPathMarker Path { get; set; }
	}
}
