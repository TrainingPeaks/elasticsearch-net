using System.Net.Sockets;

namespace ES.Net.Connection.Thrift.Transport
{
	internal class SocketConnectState
	{
		public SocketException Exception;
		public Socket Socket;

		public SocketConnectState(Socket socket)
		{
			Socket = socket;
		}
	}
}