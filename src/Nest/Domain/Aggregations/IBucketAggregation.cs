using System.Collections.Generic;

namespace Nest17
{
	public interface IBucketAggregation : IAggregation
	{
		IDictionary<string, IAggregation> Aggregations { get; }
	}
}