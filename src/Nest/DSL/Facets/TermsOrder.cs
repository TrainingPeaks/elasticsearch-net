﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest17
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TermsOrder
	{
		[EnumMember(Value = "count")]
		Count = 0,
		[EnumMember(Value = "term")]
		Term,
		[EnumMember(Value = "reverse_count")]
		ReverseCount,
		[EnumMember(Value = "reverse_term")]
		ReverseTerm
	}
}
