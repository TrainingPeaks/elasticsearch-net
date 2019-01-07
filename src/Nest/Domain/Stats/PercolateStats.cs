﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	[JsonObject]
	public class PercolateStats
	{
		[JsonProperty("total")]
		public long Total { get; set; }

		[JsonProperty("time_in_millis")]
		public long TimeInMilliseconds { get; set; }

		[JsonProperty("current")]
		public long Current { get; set; }

		[JsonProperty("memory_size_in_bytes")]
		public long MemorySizeInBytes { get; set; }

		[JsonProperty("memory_size")]
		public string MemorySize { get; set; }

		[JsonProperty("queries")]
		public long Queries { get; set; }
	}
}
