﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoPoco.Engine;

namespace Nest17.Tests.MockData.DataSources
{
	/// <summary>
	/// Generator of random booleans.
	/// </summary>
	public class BoolSource : DatasourceBase<bool>
	{
		private IntSource intSource = new IntSource();
		public override bool Next(IGenerationSession session)
		{
			return (intSource.Next(session) % 2) == 0;
		}
	}
}
