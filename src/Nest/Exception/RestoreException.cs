using System;
using ES.Net;

namespace Nest17
{
	public class RestoreException : Exception
	{
		public IElasticsearchResponse Status { get; private set; }

		public RestoreException(IElasticsearchResponse status, string message = null)
			: base(message)
		{
			this.Status = status;
		}
	}
}