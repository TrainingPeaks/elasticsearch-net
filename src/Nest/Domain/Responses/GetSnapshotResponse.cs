using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest17
{
	public interface IGetSnapshotResponse : IResponse
	{
		[JsonProperty("snapshots")]
		IEnumerable<Snapshot> Snapshots { get; set; }
	}

	[JsonObject]
	public class GetSnapshotResponse : BaseResponse, IGetSnapshotResponse
	{

		[JsonProperty("snapshots")]
		public IEnumerable<Snapshot> Snapshots { get; set; }

	}
}
