﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Nest17
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RoutingAllocationEnableOption
	{
		[EnumMember(Value = "all")]
		All,
		[EnumMember(Value = "primaries")]
		Primaries,
		[EnumMember(Value = "new_primaries")]
		NewPrimaries,
		[EnumMember(Value = "none")]
		None
	}
}
