﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ES.Net;
using ES.Net.Connection;
using ES.Net.Connection.Configuration;
using ES.Net.Exceptions;

namespace Nest17
{
	/// <summary>
	/// ElasticClient is NEST's strongly typed client which exposes fully mapped elasticsearch endpoints
	/// </summary>
	public partial class ElasticClient : IElasticClient, IHighLevelToLowLevelDispatcher
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		internal IHighLevelToLowLevelDispatcher Dispatcher { get { return this; } }
		internal RawDispatch RawDispatch { get; set; }
		
		public IConnection Connection { get; protected set; }
		public INestSerializer Serializer { get; protected set; }
		public IElasticsearchClient Raw { get; protected set; }
		public ElasticInferrer Infer { get; protected set; }


		/// <summary>
		/// Instantiate a new strongly typed connection to elasticsearch
		/// </summary>
		/// <param name="settings">An optional settings object telling the client how and where to connect to.
		/// <para>Defaults to a static single node connection pool to http://localhost:9200</para>
		/// <para>It's recommended to pass an explicit 'new ConnectionSettings()' instance</para>
		/// </param>
		/// <param name="connection">Optionally provide a different connection handler, defaults to http using HttpWebRequest</param>
		/// <param name="serializer">Optionally provide a custom serializer responsible for taking a stream and turning into T</param>
		/// <param name="transport">The transport coordinates requests between the client and the connection pool and the connection</param>
		public ElasticClient(
			IConnectionSettingsValues settings = null,
			IConnection connection = null,
			INestSerializer serializer = null,
			ITransport transport = null)
		{
			this._connectionSettings = settings ?? new ConnectionSettings();
			this.Connection = connection ?? new HttpConnection(this._connectionSettings);

			this.Serializer = serializer ?? new NestSerializer(this._connectionSettings);
			this.Raw = new ElasticsearchClient(
				this._connectionSettings,
				this.Connection,
				transport, //default transport
				this.Serializer
			);
			this.RawDispatch = new RawDispatch(this.Raw);
			this.Infer = this._connectionSettings.Inferrer;

		}

		public static void Warmup()
		{
			var client = new ElasticClient(connection: new InMemoryConnection());
			var stream = new MemoryStream("{}".Utf8Bytes());
			client.Serializer.Serialize(new SearchDescriptor<object>());
			client.Serializer.Deserialize<SearchDescriptor<object>>(stream);
			var connection = new HttpConnection(new ConnectionSettings());
			client.RootNodeInfo();
			client.Search<object>(s => s.MatchAll().Index("someindex"));
		}

		R IHighLevelToLowLevelDispatcher.Dispatch<D, Q, R>(D descriptor, Func<ElasticsearchPathInfo<Q>, D, ElasticsearchResponse<R>> dispatch)
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			var response = dispatch(pathInfo, descriptor);
			return ResultsSelector<D, Q, R>(response, descriptor);
		}

		R IHighLevelToLowLevelDispatcher.Dispatch<D, Q, R>(Func<D, D> selector, Func<ElasticsearchPathInfo<Q>, D, ElasticsearchResponse<R>> dispatch)
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return this.Dispatcher.Dispatch<D, Q, R>(descriptor, dispatch);
		}

		Task<I> IHighLevelToLowLevelDispatcher.DispatchAsync<D, Q, R, I>(D descriptor, Func<ElasticsearchPathInfo<Q>, D, Task<ElasticsearchResponse<R>>> dispatch)
		{
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return dispatch(pathInfo, descriptor)
				.ContinueWith<I>(r =>
				{
					if (r.IsFaulted && r.Exception != null)
					{
						var mr = r.Exception.InnerException as MaxRetryException;
						if (mr != null)
							mr.RethrowKeepingStackTrace();

						var ae = r.Exception.Flatten();
						if (ae.InnerException != null)
							ae.InnerException.RethrowKeepingStackTrace();

						ae.RethrowKeepingStackTrace();
					}
					return ResultsSelector<D, Q, R>(r.Result, descriptor);
				});
		}

		Task<I> IHighLevelToLowLevelDispatcher.DispatchAsync<D, Q, R, I>(Func<D, D> selector, Func<ElasticsearchPathInfo<Q>, D, Task<ElasticsearchResponse<R>>> dispatch)
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new D());
			return this.Dispatcher.DispatchAsync<D, Q, R, I>(descriptor, dispatch);
		}

		private static R ResultsSelector<D, Q, R>(
			ElasticsearchResponse<R> c,
			D descriptor
			)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>
			where R : BaseResponse
		{
			var config = descriptor.RequestConfiguration;
			var statusCodeAllowed = config != null && config.AllowedStatusCodes.HasAny(i => i == c.HttpStatusCode);

			if (c.Success || statusCodeAllowed)
			{
				c.Response.IsValid = true;
				return c.Response;
			}
			var badResponse = CreateInvalidInstance<R>(c);
			return badResponse;
		}

		private static R CreateInvalidInstance<R>(IElasticsearchResponse response) where R : BaseResponse
		{
			var r = (R)typeof(R).CreateInstance();
			((IResponseWithRequestInformation)r).RequestInformation = response;
			r.IsValid = false;
			return r;
		}

		private TRequest ForceConfiguration<TRequest>(
			Func<TRequest, TRequest> selector, Action<IRequestConfiguration> setter
			)
			where TRequest : class, IRequest, new()
		{
			selector = selector ?? (s => s);
			var request = selector(new TRequest());
			return ForceConfiguration(request, setter);
		}

		private TRequest ForceConfiguration<TRequest>(TRequest request, Action<IRequestConfiguration> setter)
			where TRequest : IRequest
		{
			var configuration = request.RequestConfiguration ?? new RequestConfiguration();
			setter(configuration);
			request.RequestConfiguration = configuration;
			return request;
		}
	}
}
