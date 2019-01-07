using Newtonsoft.Json;

namespace Nest17
{
	public class RecoveryFileDetails
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("length")]
		public long Length { get; internal set; }

		[JsonProperty("recovered")]
		public long Recovered { get; internal set; }

	}
}