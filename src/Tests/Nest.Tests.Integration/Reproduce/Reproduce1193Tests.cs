using ES.Net.ConnectionPool;
using Nest17;
using Nest17.Tests.Integration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nest17.Tests.Integration.Reproduce
{
	[TestFixture]
	[Ignore]
	public class Reproduce1193Tests
	{
		[Test]
		public void SniffingConnectionPoolPingThrowsException()
		{
			//ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;
			var nodes = new Uri[] { new Uri("https://localhost:9200") };
			var pool = new SniffingConnectionPool(nodes);
			var settings = new ConnectionSettings(pool)
				.SetBasicAuthentication("netuser", "admin")
				.SniffOnStartup();

			Assert.DoesNotThrow(() => new ElasticClient(settings));
		}
	}
}
