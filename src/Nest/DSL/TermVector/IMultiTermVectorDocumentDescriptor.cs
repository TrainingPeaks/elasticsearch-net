namespace Nest17
{
	public interface IMultiTermVectorDocumentDescriptor
	{
		MultiTermVectorDocument Document { get; set; }
		MultiTermVectorDocument GetDocument(); 
	}
}