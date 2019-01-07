﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Core.Versioning
{
	[TestFixture]
	public class VersioningTests : BaseJsonTests
	{
		[Test]
		public void IndexSupportsVersioning()
		{
			var o = new ElasticsearchProject { Id = 1, Name = "Test" };
			var result = this._client.Index(o, i=>i.Version(1));
			var status = result.ConnectionStatus;
			StringAssert.Contains("version=1", status.RequestUrl);
		}
		
		[Test]
		public void NestSupportsLongs()
		{
			var o = new ElasticsearchProject { Id = 1, Name = "Test" };
			var versionString = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
			var version = long.Parse(versionString);

			var result = this._client.Index(o, i=>i.Version(version));
			var status = result.ConnectionStatus;
			StringAssert.Contains("version=" + versionString, status.RequestUrl);
		}
		
		[Test]
		public void ElasticsearchNETSupportsLongs()
		{
			var o = new ElasticsearchProject { Id = 1, Name = "Test" };
			var versionString = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
			var version = long.Parse(versionString);

			var result = this._client.Raw.Index("index", "type", o, i=>i.Version(version));
			StringAssert.Contains("version=" + versionString, result.RequestUrl);
		}
        [Test]
        public void IndexOpTypeDefault()
        {
            var o = new ElasticsearchProject { Id = 1, Name = "Test" };
            var result = this._client.Index(o);
            var status = result.ConnectionStatus;
            StringAssert.DoesNotContain("op_type=create", status.RequestUrl);
        }

        [Test]
        public void IndexOpTypeCreate()
        {
            var o = new ElasticsearchProject { Id = 1, Name = "Test" };
            var result = this._client.Index(o, i => i.OpType(OpType.Create));
            var status = result.ConnectionStatus;
            StringAssert.Contains("op_type=create", status.RequestUrl);
        }
	}
}
