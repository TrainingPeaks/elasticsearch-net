﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17.Resolvers.Converters.Queries
{
	public class DismaxQueryJsonConverter : ReadAsTypeConverter<DisMaxQueryDescriptor<object>>
	{
		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		public override bool CanRead { get { return true; } }

		public override bool CanWrite { get { return true; } }

		public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
		{
			return base.ReadJson(reader, objectType, existingValue, serializer);
		}

		public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
			var d = value as IDisMaxQuery;
			if (d == null) return;

			writer.WriteStartObject();
			{
				if (!d.Name.IsNullOrEmpty()) {
					writer.WritePropertyName("_name");
					writer.WriteValue(d.Name);
				}
				if (d.TieBreaker.HasValue)
				{
					writer.WritePropertyName("tie_breaker");
					serializer.Serialize(writer, d.TieBreaker);
				}
				if (d.Boost.HasValue)
				{
					writer.WritePropertyName("boost");
					writer.WriteValue(d.Boost.Value);
				}
				if (d.Queries.HasAny() && d.Queries.Any(q => !q.IsConditionless))
				{
					writer.WritePropertyName("queries");
					writer.WriteStartArray();
					{
						foreach (var q in d.Queries)
						{
							if (!q.IsConditionless)
							{
								serializer.Serialize(writer, q);
							}
						}
					}
					writer.WriteEndArray();
				}
			}
			writer.WriteEndObject();
		}
	}
}
