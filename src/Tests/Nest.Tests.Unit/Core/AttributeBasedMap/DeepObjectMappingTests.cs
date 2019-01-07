﻿using NUnit.Framework;
using System.Collections.Generic;

namespace Nest17.Tests.Unit.Core.AttributeBasedMap
{
	[TestFixture]
	public class DeepObjectMappingTests : BaseAttributeMappingTests
	{
		private class NestedMapParent
		{
			public string Name { get; set; }
			public NestedMapChild Child { get; set; }
		}
		private class NestedMapChild
		{
			public string Name { get; set; }
			public NestedMapChildChild Child { get; set; }
		}
		private class NestedMapChildChild
		{
			public string Name { get; set; }
		}
		private class NestedRecursiveMapParent
		{
			public string Name { get; set; }
			public NestedRecursiveMapChild Child { get; set; }
		}
		private class NestedRecursiveMapChild
		{
			public string Name { get; set; }
			public NestedRecursiveMapParent Parent { get; set; }
		}
		private class RecursiveMapCollection
		{
			public string Name { get; set; }
			public IEnumerable<RecursiveMapCollection> Items { get; set; }
		}

		[Test]
		public void TestDeepObjectWriter()
		{ 
			var json = this.CreateMapFor<NestedMapParent>();
			this.JsonEquals(json, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void TestDeepObjectResursiveWriter()
		{
			//make sure we dont stackoverflow because of a never ending recursion
		  Assert.DoesNotThrow(() => this.CreateMapFor<NestedRecursiveMapParent>() );
		}
		[Test]
		public void TestRecursiveCollectionWriter()
		{
			Assert.DoesNotThrow(() => this.CreateMapFor<RecursiveMapCollection>());
		}
	}
}
