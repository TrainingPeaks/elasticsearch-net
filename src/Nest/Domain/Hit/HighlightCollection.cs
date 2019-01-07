﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest17
{

	[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
	public class HighlightFieldDictionary : Dictionary<string, Highlight>
	{
		public HighlightFieldDictionary(IDictionary<string, Highlight> dictionary = null)
		{
			if (dictionary == null)
				return;
			foreach(var kv in dictionary)
			{
				this.Add(kv.Key, kv.Value);
			}
		}
	}

	[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
	public class HighlightDocumentDictionary : Dictionary<string, HighlightFieldDictionary>
	{

	}
}