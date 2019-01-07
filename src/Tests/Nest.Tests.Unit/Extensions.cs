﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest17.Tests.Unit
{
	public static class JsonExtensions
	{
		internal static bool JsonEquals(this string json, string otherjson)
		{
			var nJson = JObject.Parse(json);
			var nOtherJson = JObject.Parse(otherjson);
			return JToken.DeepEquals(nJson, nOtherJson);
		}
	}
}
