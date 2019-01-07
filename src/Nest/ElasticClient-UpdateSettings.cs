﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ES.Net;

namespace Nest17
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAcknowledgedResponse UpdateSettings(Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector)
		{
			return this.Dispatcher.Dispatch<UpdateSettingsDescriptor, UpdateSettingsRequestParameters, AcknowledgedResponse>(
				updateSettingsSelector,
				(p, d) => this.RawDispatch.IndicesPutSettingsDispatch<AcknowledgedResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IAcknowledgedResponse UpdateSettings(IUpdateSettingsRequest updateSettingsRequest)
		{
			return this.Dispatcher.Dispatch<IUpdateSettingsRequest, UpdateSettingsRequestParameters, AcknowledgedResponse>(
				updateSettingsRequest,
				(p, d) => this.RawDispatch.IndicesPutSettingsDispatch<AcknowledgedResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> UpdateSettingsAsync(Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector)
		{
			return this.Dispatcher.DispatchAsync<UpdateSettingsDescriptor, UpdateSettingsRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
					updateSettingsSelector,
					(p, d) => this.RawDispatch.IndicesPutSettingsDispatchAsync<AcknowledgedResponse>(p, d)
				);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> UpdateSettingsAsync(IUpdateSettingsRequest updateSettingsRequest)
		{
			return this.Dispatcher.DispatchAsync<IUpdateSettingsRequest, UpdateSettingsRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
					updateSettingsRequest,
					(p, d) => this.RawDispatch.IndicesPutSettingsDispatchAsync<AcknowledgedResponse>(p, d)
				);
		}

	}
}