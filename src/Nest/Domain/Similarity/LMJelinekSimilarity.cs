﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest17
{
	public class LMJelinekSimilarity : SimilarityBase
	{
		public LMJelinekSimilarity()
		{
			this.Type = "LMJelinekMercer";
		}

		[JsonProperty("lambda")]
		public double Lambda { get; set; }
	}
}
