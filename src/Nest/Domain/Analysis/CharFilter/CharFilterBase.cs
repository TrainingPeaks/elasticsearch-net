using Newtonsoft.Json;

namespace Nest17
{
    public abstract class CharFilterBase : IAnalysisSetting
    {
        protected CharFilterBase(string type)
        {
            this.Type = type;
        }
		
		[JsonProperty("version")]
	    public string Version { get; set; }

	    [JsonProperty("type")]
        public string Type { get; protected set; }
    }
}