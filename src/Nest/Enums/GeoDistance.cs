﻿using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest17
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GeoDistance
	{
		[EnumMember(Value = "sloppy_arc")]
		SloppyArc,
		[EnumMember(Value = "arc")]
		Arc,
		[EnumMember(Value = "plane")]
		Plane
	}
}