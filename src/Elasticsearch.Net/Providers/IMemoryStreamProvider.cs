using System.IO;

namespace ES.Net.Providers
{
	public interface IMemoryStreamProvider
	{
		MemoryStream New();
	}
}