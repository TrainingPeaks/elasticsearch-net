using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest17
{
	public class DefaultSimilarity : SimilarityBase
	{
		public DefaultSimilarity()
		{
			this.Type = "default";
		}

		[JsonProperty("discount_overlaps")]
		public bool DiscountOverlaps { get; set; }
	}
}
