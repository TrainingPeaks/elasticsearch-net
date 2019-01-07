﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest17
{
	public class AllocateClusterRerouteCommandDescriptor
	{
		internal AllocateClusteRerouteCommand Command = new AllocateClusteRerouteCommand();

		public AllocateClusterRerouteCommandDescriptor Index(string index)
		{
			this.Command.Index = index;
			return this;
		}

		public AllocateClusterRerouteCommandDescriptor Shard(int shard)
		{
			this.Command.Shard = shard;
			return this;
		}

		public AllocateClusterRerouteCommandDescriptor Node(string node)
		{
			this.Command.Node = node;
			return this;
		}

		public AllocateClusterRerouteCommandDescriptor AllowPrimary(bool allowPrimary = true)
		{
			this.Command.AllowPrimary = allowPrimary;
			return this;
		}
	}
}
