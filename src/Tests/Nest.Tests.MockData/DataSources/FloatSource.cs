﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoPoco.Engine;

namespace Nest17.Tests.MockData.DataSources
{
	public class FloatSource : DatasourceBase<float>
	{
		private Random mRandom = new Random(1337);
		public override float Next(IGenerationSession session)
		{
			float f = mRandom.Next(0, 100);
			f = f + (float)mRandom.NextDouble();
			return f;
		}

	}
}
