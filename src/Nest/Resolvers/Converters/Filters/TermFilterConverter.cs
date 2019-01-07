﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest17.Resolvers.Converters.Filters
{
	public class TermFilterConverter : JsonConverter
	{
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return true; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var f = value as ITermFilter;
			if (f == null || (f.IsConditionless && !f.IsVerbatim)) return;
			
			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null)
				return;
			
			var field = contract.Infer.PropertyPath(f.Field);
			if (field.IsNullOrEmpty())
				return;

			writer.WriteStartObject();
			{
				WriteProperty(writer, serializer, field, f.Value);
				WriteProperty(writer, serializer, "boost", f.Boost);
				WriteProperty(writer, serializer, "_cache", f.Cache);
				WriteProperty(writer, serializer, "_cache_key", f.CacheKey);
				WriteProperty(writer, serializer, "_name", f.FilterName);
				
			}
			writer.WriteEndObject();
		}

		private static void WriteProperty(JsonWriter writer, JsonSerializer serializer, string field, object value)
		{
			if ((field.IsNullOrEmpty() || value == null))
				return;
			writer.WritePropertyName(field);
			serializer.Serialize(writer, value);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (j == null || !j.HasValues)
				return null;

			ITermFilter filter = new TermFilter();
			foreach (var jv in j)
			{
				switch (jv.Key)
				{
					case "boost":
						filter.Boost = jv.Value.Value<double?>();
						break;
					case "_cache":
						filter.Cache = jv.Value.Value<bool?>();
						break;
					case "_cacheKey":
					case "_cache_key":
						filter.CacheKey = jv.Value.Value<string>();
						break;
					case "_name":
						filter.FilterName = jv.Value.Value<string>();
						break;
					default:
						var field = jv.Key;
						filter.Field = field;
						filter.Value = jv.Value.Value<string>();
						break;
				}
			}
			return filter;

		}
	}
	
}
