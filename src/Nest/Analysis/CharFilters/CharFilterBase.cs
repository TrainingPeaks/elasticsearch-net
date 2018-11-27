﻿using System.Runtime.Serialization;

namespace Nest
{
	[ContractJsonConverter(typeof(CharFilterJsonConverter))]
	public interface ICharFilter
	{
		[DataMember(Name ="type")]
		string Type { get; }

		[DataMember(Name ="version")]
		string Version { get; set; }
	}


	public abstract class CharFilterBase : ICharFilter
	{
		protected CharFilterBase(string type) => Type = type;

		public string Type { get; protected set; }
		public string Version { get; set; }
	}

	public abstract class CharFilterDescriptorBase<TCharFilter, TCharFilterInterface>
		: DescriptorBase<TCharFilter, TCharFilterInterface>, ICharFilter
		where TCharFilter : CharFilterDescriptorBase<TCharFilter, TCharFilterInterface>, TCharFilterInterface
		where TCharFilterInterface : class, ICharFilter
	{
		protected abstract string Type { get; }
		string ICharFilter.Type => Type;
		string ICharFilter.Version { get; set; }

		public TCharFilter Version(string version) => Assign(a => a.Version = version);
	}
}
