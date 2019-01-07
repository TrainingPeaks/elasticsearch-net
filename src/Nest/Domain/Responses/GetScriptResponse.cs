﻿using Newtonsoft.Json;

namespace Nest17
{
    public interface IGetScriptResponse : IResponse
    {
        string Script { get; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class GetScriptResponse : BaseResponse, IGetScriptResponse
    {
        [JsonProperty("script")]
        public string Script { get; set; }
    }
}