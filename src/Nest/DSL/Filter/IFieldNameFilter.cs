namespace Nest17
{
	public interface IFieldNameFilter : IFilter
	{
		PropertyPathMarker Field { get; set; }
	}
}