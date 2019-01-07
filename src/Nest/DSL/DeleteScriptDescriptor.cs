using System;
using ES.Net;
using Newtonsoft.Json;

namespace Nest17
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface IDeleteScriptRequest : IRequest<DeleteScriptRequestParameters>
    {
        [JsonProperty("lang")]
        string Lang { get; set; }
        [JsonProperty("id")]
        string Id { get; set; }
    }

    public partial class DeleteScriptRequest : BasePathRequest<DeleteScriptRequestParameters>, IDeleteScriptRequest
    {
        public string Lang { get; set; }
        public string Id { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteScriptRequestParameters> pathInfo)
        {
            DeleteScriptPathInfo.Update(pathInfo, this);
        }
    }

    internal static class DeleteScriptPathInfo
    {
        public static void Update(ElasticsearchPathInfo<DeleteScriptRequestParameters> pathInfo, IDeleteScriptRequest request)
        {
            pathInfo.Id = request.Id;
            pathInfo.Lang = request.Lang;
            pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
        }
    }

    [DescriptorFor("ScriptDelete")]
    public partial class DeleteScriptDescriptor : BasePathDescriptor<DeleteScriptDescriptor, DeleteScriptRequestParameters>, IDeleteScriptRequest
    {
        IDeleteScriptRequest Self { get { return this; } }
        string IDeleteScriptRequest.Lang { get; set; }
        string IDeleteScriptRequest.Id { get; set; }

        public DeleteScriptDescriptor Id(string id)
        {
            this.Self.Id = id;
            return this;
        }

        public DeleteScriptDescriptor Lang(ScriptLang lang)
        {
            this.Self.Lang = lang.GetStringValue();
            return this;
        }

        public DeleteScriptDescriptor Lang(string lang)
        {
            this.Self.Lang = lang;
            return this;
        }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteScriptRequestParameters> pathInfo)
        {
            DeleteScriptPathInfo.Update(pathInfo, this);
        }
    }
}