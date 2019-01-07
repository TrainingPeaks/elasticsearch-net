using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest17
{
	public interface IElasticType : IFieldMapping
	{
		PropertyNameMarker Name { get; set; }
		TypeNameMarker Type { get; }
	}
}
