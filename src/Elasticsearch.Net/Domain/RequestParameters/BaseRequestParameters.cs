﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using ES.Net.Connection;
using ES.Net.Connection.Configuration;

namespace ES.Net
{
	public class RequestParameters : IRequestParameters
	{
		public IDictionary<string, object> QueryString { get; set; }
		public Func<IElasticsearchResponse, Stream, object> DeserializationState { get; set; }
		public IRequestConfiguration RequestConfiguration { get; set; }
		
		public RequestParameters()
		{
			this.QueryString = new Dictionary<string, object>();
		}

		public TOut GetQueryStringValue<TOut>(string name)
		{
			if (!this.QueryString.ContainsKey(name))
				return default(TOut);
			var value = this.QueryString[name];
			if (value == null)
				return default(TOut);
			return (TOut)value;
		}
	}
}