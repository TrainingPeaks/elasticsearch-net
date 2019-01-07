﻿using ES.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	public interface ICatResponse<TCatRectord> : IResponse
		where TCatRectord : ICatRecord
	{
		IEnumerable<TCatRectord> Records { get; }
	}

	[JsonObject]
	public class CatResponse<TCatRecord> : BaseResponse, ICatResponse<TCatRecord>
		where TCatRecord : ICatRecord
	{
		public IEnumerable<TCatRecord> Records { get; internal set; }
	}
}
