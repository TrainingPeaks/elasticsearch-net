using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest17.Resolvers.Converters;

namespace Nest17
{
	[JsonConverter(typeof(GeoPrecisionConverter))]
	public class GeoPrecision
	{
		public double Precision { get; private set; }
		public GeoPrecisionUnit Unit { get; private set; }

		public GeoPrecision(double precision, GeoPrecisionUnit unit)
		{
			this.Precision = precision;
			this.Unit = unit;
		}
	}
}
