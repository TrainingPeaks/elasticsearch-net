﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nest17.Tests.Unit
{
	public static class TestElasticClient
	{
		public static ElasticClient Client;
		public static ConnectionSettings Settings;
		static TestElasticClient()
		{
			Settings = new ConnectionSettings(UnitTestDefaults.Uri, UnitTestDefaults.DefaultIndex);

			Client = new ElasticClient(Settings);
		}

		public static string Serialize<T>(T obj) where T : class
		{
			return Encoding.UTF8.GetString(Client.Serializer.Serialize(obj));
		}

		public static T Deserialize<T>(string json) where T : class
		{
			return Client.Serializer.Deserialize<T>(new MemoryStream(Encoding.UTF8.GetBytes(json)));
		}
	}
}
