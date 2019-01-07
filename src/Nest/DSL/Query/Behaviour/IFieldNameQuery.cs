namespace Nest17.DSL.Query.Behaviour
{
	public interface IFieldNameQuery : IQuery
	{
		PropertyPathMarker GetFieldName();
		void SetFieldName(string fieldName);
	}
}