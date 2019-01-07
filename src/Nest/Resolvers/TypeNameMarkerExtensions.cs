﻿namespace Nest17
{
	public static class TypeNameMarkerExtensions
	{
		
		public static bool IsConditionless(this TypeNameMarker marker)
		{
			return marker == null || marker.Name.IsNullOrEmpty() && marker.Type == null;
		}
	}
}