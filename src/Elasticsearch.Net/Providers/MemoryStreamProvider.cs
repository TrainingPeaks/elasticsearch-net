using System.IO;

namespace ES.Net.Providers
{
	public class MemoryStreamProvider : IMemoryStreamProvider
	{
		public MemoryStream New()
		{
			return new MemoryStream();
		}
	}
}