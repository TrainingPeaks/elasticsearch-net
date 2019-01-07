﻿using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest17
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum NormsLoading
	{
		[EnumMember(Value = "lazy")]
		Lazy,
		[EnumMember(Value = "eager")]
		Eager
	}
}
