﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	[JsonObject(MemberSerialization.OptIn)]
	public class FieldDataFrequencyFilter
	{
		[JsonProperty("min")]
		public double? Min { get; set; }

		[JsonProperty("max")]
		public double? Max { get; set; }

		[JsonProperty("min_segment_size")]
		public int? MinSegmentSize { get; set; }
	}
}
