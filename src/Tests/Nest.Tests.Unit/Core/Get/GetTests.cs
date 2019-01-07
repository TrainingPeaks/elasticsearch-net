﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Net;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Core.Get
{
	[TestFixture]
	public class GetTests : BaseJsonTests
	{
		[Test]
		public void GetSimple()
		{
			var result = this._client.Get<ElasticsearchProject>(g=>g.Id(1));
			var status = result.ConnectionStatus;
			StringAssert.Contains("using Nest17 IN MEMORY CONNECTION", 
				result.ConnectionStatus.ResponseRaw.Utf8String());
			StringAssert.EndsWith("/nest_test_data/elasticsearchprojects/1", status.RequestUrl);
		}
		
		[Test]
		public void GetUsingDescriptor()
		{
			var result = this._client.Get<ElasticsearchProject>(g=>g
				.Index("myindex")
				.Id(404)
			);
			var status = result.ConnectionStatus;
			StringAssert.EndsWith("/myindex/elasticsearchprojects/404", status.RequestUrl);
		}
	
		[Test]
		public void GetUsingDescriptorWithType()
		{
			var result = this._client.Get<ElasticsearchProject>(g => g
				.Index("myindex")
				.Type("mytype")
				.Id(404)
			);
			var status = result.ConnectionStatus;
			StringAssert.EndsWith("/myindex/mytype/404", status.RequestUrl);
		}
		[Test]
		public void GetUsingDescriptorWithTypeAndFields()
		{
			var result = this._client.Get<ElasticsearchProject>(g => g
				.Index("myindex")
				.Type("mytype")
				.Id(404)
				.Fields(p=>p.Content, p=>p.Name)
			);
			var status = result.ConnectionStatus;
			StringAssert.EndsWith("/myindex/mytype/404?fields=content%2Cname", status.RequestUrl);
		}
	}
}
