﻿using Newtonsoft.Json;

namespace Nest17
{
	public abstract class FacetItem
	{
		[JsonProperty("count")]
		public virtual long Count { get; internal set; }
	}
}