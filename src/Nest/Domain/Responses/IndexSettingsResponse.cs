using System.Collections.Generic;
using System.Linq;
using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
	public interface IIndexSettingsResponse : IResponse
	{
		IndexSettings IndexSettings { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(IndexSettingsResponseConverter))]
	public class IndexSettingsResponse : BaseResponse, IIndexSettingsResponse
	{
		[JsonIgnore]
		public IndexSettings IndexSettings
		{
			get { return Nodes.HasAny() ? Nodes.First().Value : null; }
		}

		[JsonIgnore]
		public Dictionary<string, IndexSettings> Nodes { get; set; }

	}
}
