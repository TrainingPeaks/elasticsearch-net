using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nest17
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GeoOrientation
	{
		[EnumMember(Value = "cw")]
		ClockWise,
		[EnumMember(Value = "ccw")]
		CounterClockWise
	}
}
