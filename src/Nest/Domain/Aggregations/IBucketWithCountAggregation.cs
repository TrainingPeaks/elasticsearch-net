namespace Nest17
{
	public interface IBucketWithCountAggregation : IBucketAggregation
	{
		long DocCount { get; }
	}
}