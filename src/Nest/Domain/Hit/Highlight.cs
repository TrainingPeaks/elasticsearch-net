﻿using System.Collections.Generic;

namespace Nest17
{
	public class Highlight
	{
		public string DocumentId { get; internal set; }
		public string Field { get; internal set; }
		public IEnumerable<string> Highlights { get; set; }
	}
}
