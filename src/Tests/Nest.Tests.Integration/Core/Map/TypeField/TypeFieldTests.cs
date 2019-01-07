using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Integration.Core.Map.TypeField
{
	[TestFixture]
	public class TypeFieldTests : BaseMappingTests
	{
		[Test]
		public void TypeFieldSerializesYes()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.TypeField(t => t
					.Index()
					.Store()
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void TypeFieldSerializesNo()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.TypeField(t => t
					.Index(NonStringIndexOption.No)
					.Store(false)
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
