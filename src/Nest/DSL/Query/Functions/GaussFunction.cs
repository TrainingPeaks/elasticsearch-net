using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest17
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class GaussFunction<T> : FunctionScoreDecayFunction<T>
		where T : class
	{
		[JsonProperty(PropertyName = "gauss")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor> _GaussDescriptor { get; set; }

		public GaussFunction(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			_GaussDescriptor = new Dictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor>();

			var descriptor = new FunctionScoreDecayFieldDescriptor();
			descriptorBuilder(descriptor);
			_GaussDescriptor[objectPath] = descriptor;
		}

		public GaussFunction(string field, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			_GaussDescriptor = new Dictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor>();

			var descriptor = new FunctionScoreDecayFieldDescriptor();
			descriptorBuilder(descriptor);
			_GaussDescriptor[field] = descriptor;
		}
	}
}