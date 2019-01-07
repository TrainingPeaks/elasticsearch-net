
using Nest17.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nest17
{
	[JsonConverter(typeof(SimilaritySettingsConverter))]
	public class SimilaritySettings
	{
		public SimilaritySettings()
		{
			this.CustomSimilarities = new Dictionary<string, SimilarityBase>();
		}

		public string Default { get; set; }

		[JsonConverter(typeof(SimilarityCollectionConverter))]
		public IDictionary<string, SimilarityBase> CustomSimilarities { get; set; }
	}
}
