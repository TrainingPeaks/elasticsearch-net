﻿using System.Collections.Generic;

namespace Nest17
{
	public class ReadOnlyUrlRepository : IRepository
	{
		string IRepository.Type { get { return "url"; } }
		public IDictionary<string, object> Settings { get; set; }
	}

	public class ReadOnlyUrlRepositoryDescriptor : IRepository
	{
		string IRepository.Type { get { return "url"; } }
		IDictionary<string, object> IRepository.Settings { get; set; }

		private IRepository Self { get { return this; } }

		public ReadOnlyUrlRepositoryDescriptor()
		{
			Self.Settings = new Dictionary<string, object>();
		}
		/// <summary>
		/// Location of the snapshots. Mandatory.
		/// </summary>
		/// <param name="location"></param>
		public ReadOnlyUrlRepositoryDescriptor Location(string location)
		{
			Self.Settings["location"] = location;
			return this;
		}
		
		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public ReadOnlyUrlRepositoryDescriptor ConcurrentStreams(int concurrentStreams)
		{
			Self.Settings["concurrent_streams"] = concurrentStreams;
			return this;
		}
	
	}
}