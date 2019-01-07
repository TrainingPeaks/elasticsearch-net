﻿using ES.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ES.Net.Connection;

namespace Nest17.Tests.Unit.Reproduce
{
	[TestFixture]
	public class Reproduce1528Tests : BaseJsonTests
	{
		[ElasticType(Name ="mytype", IdProperty ="Name")]
		public class MyClass
		{
			public string Name { get; set; }
		}

		[Test]
		public void Issue1528()
		{
			var settings = new ConnectionSettings(defaultIndex: "default-index");
			var client = new ElasticClient(settings, new InMemoryConnection(settings));
			var descriptor = new BulkDescriptor();

			foreach (var i in Enumerable.Range(0, 5))
			{
				descriptor.Index<MyClass>(op => op
					.Document(new MyClass { Name = "my_id_" + i })
				);
			}
			var result = client.Bulk(descriptor);
			var jsonRequest = result.ConnectionStatus.Request;
			this.BulkJsonEquals(jsonRequest.Utf8String(), MethodBase.GetCurrentMethod());
		}

		[Test]
		public void Issue1528_NoDefaultIndex()
		{
			var settings = new ConnectionSettings();
			var client = new ElasticClient(settings, new InMemoryConnection(settings));
			var descriptor = new BulkDescriptor();

			foreach (var i in Enumerable.Range(0, 5))
			{
				descriptor.Index<MyClass>(op => op
					.Document(new MyClass { Name = "my_id_" + i })
				);
			}
			Assert.Throws<NullReferenceException>(()=>{
				client.Bulk(descriptor);
			});
		}

		[Test]
		public void Issue1528_ExplicitTypeAndIndex()
		{
			var settings = new ConnectionSettings(defaultIndex: "default-index");
			var client = new ElasticClient(settings, new InMemoryConnection(settings));
			var descriptor = new BulkDescriptor();

			foreach (var i in Enumerable.Range(0, 5))
			{
				descriptor.Index<MyClass>(op => op
					.Index("myindex")
					.Type("sometype")
					.Document(new MyClass { Name = "my_id_" + i })
				);
			}
			var result = client.Bulk(descriptor);
			var jsonRequest = result.ConnectionStatus.Request;
			this.BulkJsonEquals(jsonRequest.Utf8String(), MethodBase.GetCurrentMethod());
		}

		[Test]
		public void Issue1528_ExplicitTypeAndIndex_NoDefaultIndex()
		{
			var settings = new ConnectionSettings();
			var client = new ElasticClient(settings, new InMemoryConnection(settings));
			var descriptor = new BulkDescriptor();

			foreach (var i in Enumerable.Range(0, 5))
			{
				descriptor.Index<MyClass>(op => op
					.Index("myindex")
					.Type("sometype")
					.Document(new MyClass { Name = "my_id_" + i })
				);
			}
			var result = client.Bulk(descriptor);
			var jsonRequest = result.ConnectionStatus.Request;
			this.BulkJsonEquals(jsonRequest.Utf8String(), MethodBase.GetCurrentMethod(), "Issue1528_ExplicitTypeAndIndex");
		}

	}
}
