using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
	public class AliasDefinition
	{
		public string Name { get; set; }

		[JsonProperty("filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		public IFilterContainer Filter { get; internal set; }
	
		[JsonProperty("routing")]
		public string Routing { get; internal set; }

		[JsonProperty("index_routing")]
		public string IndexRouting { get; internal set; }
		
		[JsonProperty("search_routing")]
		public string SearchRouting { get; internal set; }
	}
}