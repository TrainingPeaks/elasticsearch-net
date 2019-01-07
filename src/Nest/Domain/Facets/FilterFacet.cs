using Newtonsoft.Json;

namespace Nest17
{
	public class FilterFacet : Facet
	{
		[JsonProperty(PropertyName = "count")]
		public long Count { get; internal set; }
	}
}
