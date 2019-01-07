namespace Nest17.DSL.Visitor
{
	public enum VisitorScope
	{
		Unknown,
		Filter,
		Query,
		Must,
		MustNot,
		Should,
		PositiveQuery,
		NegativeQuery,
		NoMatchQuery,

	}
}