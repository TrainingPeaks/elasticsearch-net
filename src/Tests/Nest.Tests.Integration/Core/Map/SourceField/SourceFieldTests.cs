using NUnit.Framework;
using Nest17.Tests.MockData.Domain;

namespace Nest17.Tests.Integration.Core.Map.SourceField
{
	[TestFixture]
	public class SourceFieldTests : BaseMappingTests
	{
		[Test]
		public void SourceFieldSerializesFully()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.SourceField(s => s
					.Enabled(false)
					.Compress()
					.CompressionThreshold("200b")
					.Excludes(new[] { "path1.*" })
					.Includes(new[] { "path2.*" })
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
