namespace Nest17
{
	/// <summary>
	/// The trim token filter trims surrounding whitespaces around a token.
	/// </summary>
	public class TrimTokenFilter : TokenFilterBase
	{
		public TrimTokenFilter()
			: base("trim")
		{

		}

	}

}