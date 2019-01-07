using Newtonsoft.Json;

namespace Nest17
{
		public class QueryFacet : Facet
    {
        [JsonProperty(PropertyName = "count")]
        public long Count { get; internal set; }
    }
}
