using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Integration.Core.Map.IdField
{
	[TestFixture]
	public class IdFieldTests : BaseMappingTests
	{
		[Test]
		public void IdFieldSerializesFully()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.IdField(i => i
					.Index("not_analyzed")
					.Path("myOtherId")
					.Store(false)
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
