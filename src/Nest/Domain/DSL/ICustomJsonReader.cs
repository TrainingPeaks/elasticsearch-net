using System;
using Newtonsoft.Json;

namespace Nest17
{
	public interface ICustomJsonReader<out T> where T : class, new()
	{
		T FromJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer);
	}
}