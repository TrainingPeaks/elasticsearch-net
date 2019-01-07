﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace Nest17.Resolvers.Converters
{
	public class GetRepositoryResponseConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);

		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var dict = new Dictionary<string, IRepository>();
			serializer.Populate(reader, dict);
			var response = new GetRepositoryResponse();
			if (!dict.HasAny())
				return response;
			response.Repositories = dict;
			return response;
		}

	}
}

