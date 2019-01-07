using NUnit.Framework;
using Nest17.Tests.MockData.Domain;
using System.Reflection;

namespace Nest17.Tests.Unit.Core.Map.FieldNamesField
{
	[TestFixture]
	public class FieldNamesFieldTests : BaseJsonTests
	{
		[Test]
		public void FieldNamesMapping()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.FieldNamesField(a => a .Enabled(false))
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodBase.GetCurrentMethod()); 
		}
	}
}
