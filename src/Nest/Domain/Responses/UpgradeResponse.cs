using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	public interface IUpgradeResponse : IResponse
	{
		[JsonProperty("_shards")]
		ShardsMetaData Shards { get; set; }
	}

	public class UpgradeResponse : BaseResponse, IUpgradeResponse
	{
		public ShardsMetaData Shards { get; set; }
	}
}
