namespace Nest17
{
	public interface IElasticPropertyVisitor
	{
		void Visit(ElasticPropertyAttribute attribute);
	}
}