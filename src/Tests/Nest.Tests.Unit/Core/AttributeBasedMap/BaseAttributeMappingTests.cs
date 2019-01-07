﻿using System;
using Nest17.Resolvers.Writers;

namespace Nest17.Tests.Unit.Core.AttributeBasedMap
{
	public abstract class BaseAttributeMappingTests : BaseJsonTests
	{
		protected string CreateMapFor<T>() where T : class
		{
			return this.CreateMapFor(typeof(T));
		}
		protected string CreateMapFor(Type t)
		{
			var type = TypeNameMarker.Create(t);
			var writer = new TypeMappingWriter(t, type, TestElasticClient.Settings, 10);

			return writer.MapFromAttributes();
		}
	}
}
