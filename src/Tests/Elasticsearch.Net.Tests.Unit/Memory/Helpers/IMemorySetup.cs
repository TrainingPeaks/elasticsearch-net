using System.Collections.Generic;

namespace ES.Net.Tests.Unit.Memory.Helpers
{
	public interface IMemorySetup<T> where T : class
	{
		List<TrackableMemoryStream> CreatedMemoryStreams { get; }
		TrackableMemoryStream ResponseStream { get; }
		ElasticsearchResponse<T> Result { get; set; }
	}
}