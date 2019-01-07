﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest17.Resolvers;
using Nest17.Resolvers.Converters;
using Nest17.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using ES.Net;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapePointFilter : IGeoShapeBaseFilter
	{
		[JsonProperty("shape")]
		IPointGeoShape Shape { get; set; }
	}

	public class GeoShapePointFilter : PlainFilter, IGeoShapePointFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.GeoShape = this;
		}

		public PropertyPathMarker Field { get; set; }
		public GeoShapeRelation? Relation { get; set; }
		public IPointGeoShape Shape { get; set; }
	}

	public class GeoShapePointFilterDescriptor : FilterBase, IGeoShapePointFilter
	{
		IGeoShapePointFilter Self { get { return this; } }

		bool IFilter.IsConditionless
		{
			get
			{
				return this.Self.Shape == null || !this.Self.Shape.Coordinates.HasAny();
			}
		}

		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		GeoShapeRelation? IGeoShapeBaseFilter.Relation { get; set; }
		IPointGeoShape IGeoShapePointFilter.Shape { get; set; }

		public GeoShapePointFilterDescriptor Coordinates(IEnumerable<double> coordinates)
		{
			if (this.Self.Shape == null)
				this.Self.Shape = new PointGeoShape();
			this.Self.Shape.Coordinates = coordinates;
			return this;
		}

		public GeoShapePointFilterDescriptor Relation(GeoShapeRelation relation)
		{
			this.Self.Relation = relation;
			return this;
		}
	}

}
