﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataRegexFilter
	{
		[JsonProperty("pattern")]
		public string Pattern { get; set; }
	}
}
