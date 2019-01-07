using System;

namespace Nest17
{
	public class SnapshotObserver : CoordinatedRequestObserver<ISnapshotStatusResponse>
	{
		public SnapshotObserver(
			Action<ISnapshotStatusResponse> onNext = null, 
			Action<Exception> onError = null,
			Action completed = null)
			: base(onNext, onError, completed)
		{
			
		}
	}
}