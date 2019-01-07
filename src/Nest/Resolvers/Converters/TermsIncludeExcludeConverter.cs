﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17.Resolvers.Converters
{
	public class TermsIncludeExcludeConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(TermsIncludeExcludeConverter));
		}

		public override bool CanRead { get { return false; } }

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var termsIncludeExclude = value as TermsIncludeExclude;

			if (termsIncludeExclude.Values != null)
			{
				serializer.Serialize(writer, termsIncludeExclude.Values);
			}
			else
			{
				writer.WriteStartObject();
				writer.WritePropertyName("pattern");
				writer.WriteValue(termsIncludeExclude.Pattern);
				if (!termsIncludeExclude.Flags.IsNullOrEmpty())
				{
					writer.WritePropertyName("flags");
					writer.WriteValue(termsIncludeExclude.Flags);
				}
				writer.WriteEndObject();
			}
		}
	}
}
