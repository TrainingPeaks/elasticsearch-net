using Nest17.Resolvers;

namespace Nest17
{
	public static class IndexNameMarkerExtensions
	{
		
		public static string Resolve(this IndexNameMarker marker, IConnectionSettingsValues connectionSettings)
		{
			if (marker == null)
				return null;
			connectionSettings.ThrowIfNull("connectionSettings");

			if (marker.Type == null)
				return marker.Name;
			return new IndexNameResolver(connectionSettings).GetIndexForType(marker.Type);
		}
	}
}