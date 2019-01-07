using System;
using ES.Net;

namespace Nest17
{
	public class SnapshotException : Exception
	{
		public IElasticsearchResponse Status { get; private set; }

		public SnapshotException(IElasticsearchResponse status, string message)
			: base(message)
		{
			Status = status;
		}
	}
}