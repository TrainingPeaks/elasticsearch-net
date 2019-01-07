﻿using System;
using System.Linq.Expressions;

namespace Nest17
{
	public class GeoShapeMappingDescriptor<T>
	{
		internal GeoShapeMapping _Mapping = new GeoShapeMapping();

		public GeoShapeMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public GeoShapeMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public GeoShapeMappingDescriptor<T> Tree(GeoTree geoTree)
		{
			this._Mapping.Tree = geoTree;
			return this;
		}

		public GeoShapeMappingDescriptor<T> TreeLevels(int treeLevels)
		{
			this._Mapping.TreeLevels = treeLevels;
			return this;
		}

		public GeoShapeMappingDescriptor<T> Precision(double precision, GeoPrecisionUnit unit)
		{
			this._Mapping.Precision = new GeoPrecision(precision, unit);
			return this;
		}

		public GeoShapeMappingDescriptor<T> Orientation(GeoOrientation orientation)
		{
			this._Mapping.Orientation = orientation;
			return this;
		}

		public GeoShapeMappingDescriptor<T> DistanceErrorPercentage(double distanceErrorPercentage)
		{
			this._Mapping.DistanceErrorPercentage = distanceErrorPercentage;
			return this;
		}
	}
}