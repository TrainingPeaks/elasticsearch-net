using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest17
{
    [JsonObject]
    public class HistogramFacet : Facet, IFacet<HistogramFacetItem>
    {
        [JsonProperty("entries")]
        public IEnumerable<HistogramFacetItem> Items { get; internal set; }
    }
    public class HistogramFacetItem : FacetItem
    {
        [JsonProperty("key")]
        public double Key { get; set; }
    }

}