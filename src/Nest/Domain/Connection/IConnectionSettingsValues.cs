using System;
using System.Collections.ObjectModel;
using System.Reflection;
using ES.Net.Connection;
using Newtonsoft.Json;

namespace Nest17
{
	public interface IConnectionSettingsValues : IConnectionConfigurationValues
	{
		ElasticInferrer Inferrer { get; }
		FluentDictionary<Type, string> DefaultIndices { get; }
		FluentDictionary<Type, string> DefaultTypeNames { get; }
		FluentDictionary<Type, string> IdProperties { get; }
		FluentDictionary<MemberInfo, PropertyMapping> PropertyMappings { get; }
		string DefaultIndex { get; }
		Func<string, string> DefaultPropertyNameInferrer { get; }
		Func<Type, string> DefaultTypeNameInferrer { get; }
		Action<JsonSerializerSettings> ModifyJsonSerializerSettings { get; }
		ReadOnlyCollection<Func<Type, JsonConverter>> ContractConverters { get; }
	}
}