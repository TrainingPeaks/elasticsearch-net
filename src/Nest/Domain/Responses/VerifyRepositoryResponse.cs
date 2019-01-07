﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nest17.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization.OptIn)]
	//[JsonConverter(typeof(GetRepositoryResponseConverter))]
	public interface IVerifyRepositoryResponse : IResponse
	{
		/// <summary>
		///  A dictionary of nodeId => nodeinfo of nodes that verified the repository
		/// </summary>
		[JsonProperty(PropertyName = "nodes")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, CompactNodeInfo> Nodes { get; set; }
	}

	[JsonObject]
	public class VerifyRepositoryResponse : BaseResponse, IVerifyRepositoryResponse
	{

		/// <summary>
		///  A dictionary of nodeId => nodeinfo of nodes that verified the repository
		/// </summary>
		public Dictionary<string, CompactNodeInfo> Nodes { get; set; }
	}
}
