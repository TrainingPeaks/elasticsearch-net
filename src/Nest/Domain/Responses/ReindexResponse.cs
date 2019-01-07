﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest17
{
	/// <summary>
	/// POCO representing the reindex response for a each step
	/// </summary>
	[JsonObject]
	public class ReindexResponse<T> : IReindexResponse<T> where T : class
	{
		public IBulkResponse BulkResponse { get; internal set; }
		public ISearchResponse<T> SearchResponse { get; internal set; }

		public int Scroll { get; internal set; }

		public bool IsValid
		{
			get
			{
				return (this.BulkResponse != null && this.BulkResponse.IsValid
					&& this.SearchResponse != null && this.SearchResponse.IsValid);
			}
		}
	}
}
