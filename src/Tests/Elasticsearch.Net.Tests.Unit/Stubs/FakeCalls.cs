using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.FakeItEasy;
using ES.Net.Connection;
using ES.Net.Connection.Configuration;
using ES.Net.Providers;
using ES.Net.Serialization;
using FakeItEasy;
using FakeItEasy.Configuration;

namespace ES.Net.Tests.Unit.Stubs
{
	public static class FakeCalls 
	{
		public static IReturnValueConfiguration<ElasticsearchResponse<Stream>> GetSyncCall(AutoFake fake)
		{
			return A.CallTo(() => 
				fake.Resolve<IConnection>().GetSync(A<Uri>.That.Not.Matches(IsSniffUrl()), null));
		}
		public static IReturnValueArgumentValidationConfiguration<Task<ElasticsearchResponse<Stream>>> GetCall(AutoFake fake)
		{
			return A.CallTo(() => fake.Resolve<IConnection>().Get(A<Uri>.That.Not.Matches(IsSniffUrl()), null));
		}
		
		public static IReturnValueConfiguration<ElasticsearchResponse<Stream>> PingAtConnectionLevel(AutoFake fake)
		{
			return A.CallTo(() => fake.Resolve<IConnection>().HeadSync(
				A<Uri>.That.Matches(IsRoot()), A<IRequestConfiguration>._));
		}


		public static IReturnValueConfiguration<Task<ElasticsearchResponse<Stream>>> PingAtConnectionLevelAsync(AutoFake fake)
		{
			return A.CallTo(() => fake.Resolve<IConnection>().Head(
				A<Uri>.That.Matches(IsRoot()), A<IRequestConfiguration>._));
		}
		
		public static IReturnValueConfiguration<ElasticsearchResponse<Stream>> Sniff(
			AutoFake fake, 
			IConnectionConfigurationValues configurationValues = null,
			IList<Uri> nodes = null)
		{
			var sniffCall = A.CallTo(() => fake.Resolve<IConnection>().GetSync(
				A<Uri>.That.Matches(IsSniffUrl()), 
				A<IRequestConfiguration>._));
			if (nodes == null) return sniffCall;
			sniffCall.ReturnsLazily(()=>
			{
				
				var stream = SniffResponse(nodes);
				var response = FakeResponse.Ok(configurationValues, "GET", "/_nodes/_all/clear", stream);
				return response;
			});

			return sniffCall;
		}

		private static Expression<Func<Uri, bool>> IsSniffUrl()
		{
			return u=>u.AbsolutePath.StartsWith("/_nodes/_all");
		}
		
		/// <summary>
		/// AbsolutePath == "" on .NET 3.5 but "/" thereafter.
		/// </summary>
		/// <returns></returns>
		private static Expression<Func<Uri, bool>> IsRoot()
		{
			return u=>u.AbsolutePath == "/" || u.AbsolutePath == "";
		}

		public static ITransport ProvideDefaultTransport(
			AutoFake fake,
			IDateTimeProvider dateTimeProvider = null,
			IMemoryStreamProvider memoryStreamProvider = null
		)
		{
			var dateTimeParam = new TypedParameter(typeof(IDateTimeProvider), dateTimeProvider);
			var memoryStreamParam = new TypedParameter(typeof(IMemoryStreamProvider), memoryStreamProvider);
			var serializerParam = new TypedParameter(typeof(IElasticsearchSerializer), null);
			return fake.Provide<ITransport, Transport>(dateTimeParam, serializerParam, memoryStreamParam);
		}

		private static readonly string SniffFormat = @" {{ ""cluster_name"" : ""insert_marvel_character"", ""nodes"" : {{ {0} }} }}";
		private static readonly string NodeFormat = @" ""{0}"" : {{ ""name"" : ""node_{1}"", ""http_address"" : ""inet[/{2}]"" }}";
		private static Stream SniffResponse(IList<Uri> nodes)
		{
			var nodesFormatted = nodes
				.Select((n, i)=> string.Format(NodeFormat, Guid.NewGuid().ToString(), i, n.Host + ":" + n.Port));
			var sniffFormatted = string.Format(SniffFormat, String.Join(",", nodesFormatted));
			return new MemoryStream(Encoding.UTF8.GetBytes(sniffFormatted));

		}

		public static ITransport ProvideRealTranportInstance(AutoFake fake, IDateTimeProvider dateTimeProvider = null)
		{
			var connection = fake.Resolve<IConnection>();
			var config = fake.Resolve<IConnectionConfigurationValues>();
			var param = new TypedParameter(typeof(IDateTimeProvider), dateTimeProvider);
			var serializerParam = new TypedParameter(typeof(IElasticsearchSerializer), null);
			return new Transport(
				config, connection, 
				serializerParam.Value as IElasticsearchSerializer, 
				param.Value as IDateTimeProvider);
		}
	}
}