using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;

namespace Nest17
{
	internal partial class RawDispatch 
	{
		protected IElasticsearchClient Raw { get; set; }

		public RawDispatch(IElasticsearchClient rawElasticClient)
		{
			this.Raw = rawElasticClient;
		}
	}
}
