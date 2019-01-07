﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest17
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TopChildrenScore
	{
		[EnumMember(Value="max")]
		Max,
		[EnumMember(Value = "sum")]
		Sum,
		[EnumMember(Value = "avg")]
		Average,
		[EnumMember(Value = "min")]
		Min
	}
}
