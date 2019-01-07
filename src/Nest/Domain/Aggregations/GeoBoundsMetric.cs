﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	public class GeoBoundsMetric : IMetricAggregation
	{
		public GeoBoundsMetric()
		{
			Bounds = new GeoBounds();
		}

		public GeoBounds Bounds { get; set; }
	}

	public class GeoBounds
	{
		public LatLon TopLeft { get; set; }
		public LatLon BottomRight { get; set; }
	}
}
