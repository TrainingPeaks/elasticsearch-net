using Newtonsoft.Json;

namespace Nest17
{
	public class RecoveryStartStatus
	{
		[JsonProperty("check_index_time")]
		public long CheckIndexTime { get; internal set; }

		[JsonProperty("total_time_in_millis")]
		public string TotalTimeInMilliseconds { get; internal set; }
	}
}