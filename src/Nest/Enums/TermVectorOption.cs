﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nest17
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TermVectorOption
	{
		[EnumMember(Value = "no")]
		No,
		[EnumMember(Value = "yes")]
		Yes,
		[EnumMember(Value = "with_offsets")]
		WithOffsets,
		[EnumMember(Value = "with_positions")]
		WithPositions,
		[EnumMember(Value = "with_positions_offsets")]
		WithPositionsOffsets,
		[EnumMember(Value = "with_positions_offsets_payloads")]
        WithPositionsOffsetsPayloads
	}
}
