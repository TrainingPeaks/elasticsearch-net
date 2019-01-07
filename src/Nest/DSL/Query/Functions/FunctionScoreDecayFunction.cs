using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreDecayFunction<T> : FunctionScoreFunction<T>
		where T : class
	{
	}
}