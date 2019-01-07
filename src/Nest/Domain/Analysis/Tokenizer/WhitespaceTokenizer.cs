using System.Collections.Generic;

namespace Nest17
{
	/// <summary>
	/// A tokenizer of type whitespace that divides text at whitespace.
	/// </summary>
	public class WhitespaceTokenizer : TokenizerBase
    {
		public WhitespaceTokenizer()
        {
            Type = "whitespace";
        }
    }
}