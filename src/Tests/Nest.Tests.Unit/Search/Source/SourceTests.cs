using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Unit.Search.Source
{
	[TestFixture]
	public class SourceTests : BaseJsonTests
	{
		[Test]
		public void SourceSerializes()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .From(0)
			  .Size(10)
			  .Source(source=>source
				.Include(p=>p.Name, p=>p.NestedFollowers)
				.Exclude(p=>p.NestedFollowers.First().DateOfBirth)
			  );
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}