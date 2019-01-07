using System.Security.Cryptography.X509Certificates;
using ES.Net;

namespace Nest17
{
	public interface IPathInfo<TParameters> 
		where TParameters : IRequestParameters, new()
	{
		ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings);
	}
}