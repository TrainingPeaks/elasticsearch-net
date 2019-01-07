using System;
using System.Collections.Generic;
using System.Linq;
using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(GetRepositoryResponseConverter))]
	public interface IGetRepositoryResponse : IResponse
	{
		IDictionary<string, IRepository> Repositories { get; set; }
	}

	[JsonObject]
	public class GetRepositoryResponse : BaseResponse, IGetRepositoryResponse
	{
		public GetRepositoryResponse()
		{
			this.Repositories = new Dictionary<string, IRepository>();
		}

		public IDictionary<string, IRepository> Repositories { get; set; }
	}
}
