﻿using ES.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest17
{
	public partial class ElasticClient
	{
		public IUpgradeResponse Upgrade(IUpgradeRequest upgradeRequest)
		{
			return this.Dispatcher.Dispatch<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse>(
				upgradeRequest,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatch<UpgradeResponse>(p)
			);	
		}

		public IUpgradeResponse Upgrade(Func<UpgradeDescriptor, UpgradeDescriptor> upgradeDescriptor = null)
		{
			upgradeDescriptor = upgradeDescriptor ?? (s => s);
			return this.Dispatcher.Dispatch<UpgradeDescriptor, UpgradeRequestParameters, UpgradeResponse>(
				upgradeDescriptor,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatch<UpgradeResponse>(p)
			);
		}

		public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest upgradeRequest)
		{
			return this.Dispatcher.DispatchAsync<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse, IUpgradeResponse>(
				upgradeRequest,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatchAsync<UpgradeResponse>(p)
			);
		}

		public Task<IUpgradeResponse> UpgradeAsync(Func<UpgradeDescriptor, UpgradeDescriptor> upgradeDescriptor = null)
		{
			upgradeDescriptor = upgradeDescriptor ?? (s => s);
			return this.Dispatcher.DispatchAsync<UpgradeDescriptor, UpgradeRequestParameters, UpgradeResponse, IUpgradeResponse>(
				upgradeDescriptor,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatchAsync<UpgradeResponse>(p)
			);
		}

		public IUpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest upgradeStatusRequest)
		{
			return this.Dispatcher.Dispatch<IUpgradeStatusRequest, UpgradeStatusRequestParameters, UpgradeStatusResponse>(
				upgradeStatusRequest,
				(p, d) => this.RawDispatch.IndicesGetUpgradeDispatch<UpgradeStatusResponse>(p)
			);	
		}

		public IUpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, UpgradeStatusDescriptor> upgradeStatusDescriptor = null)
		{
			upgradeStatusDescriptor = upgradeStatusDescriptor ?? (s => s);
			return this.Dispatcher.Dispatch<UpgradeStatusDescriptor, UpgradeStatusRequestParameters, UpgradeStatusResponse>(
				upgradeStatusDescriptor,
				(p, d) => this.RawDispatch.IndicesGetUpgradeDispatch<UpgradeStatusResponse>(p)
			);
		}

		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest upgradeStatusRequest)
		{
			return this.Dispatcher.DispatchAsync<IUpgradeStatusRequest, UpgradeStatusRequestParameters, UpgradeStatusResponse, IUpgradeStatusResponse>(
				upgradeStatusRequest,
				(p, d) => this.RawDispatch.IndicesGetUpgradeDispatchAsync<UpgradeStatusResponse>(p)
			);
		}

		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, UpgradeStatusDescriptor> upgradeStatusDescriptor = null)
		{
			upgradeStatusDescriptor = upgradeStatusDescriptor ?? (s => s);
			return this.Dispatcher.DispatchAsync<UpgradeStatusDescriptor, UpgradeStatusRequestParameters, UpgradeStatusResponse, IUpgradeStatusResponse>(
				upgradeStatusDescriptor,
				(p, d) => this.RawDispatch.IndicesGetUpgradeDispatchAsync<UpgradeStatusResponse>(p)
			);
		}
	}
}
