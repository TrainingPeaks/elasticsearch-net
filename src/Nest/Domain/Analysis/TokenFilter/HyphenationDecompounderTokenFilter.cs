namespace Nest17
{
	public class HyphenationDecompounderTokenFilter : CompoundWordTokenFilter
	{
		public HyphenationDecompounderTokenFilter()
			: base("hyphenation_decompounder")
		{
		}
	}
}