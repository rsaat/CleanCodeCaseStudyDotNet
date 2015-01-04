using System.Net.Sockets;

namespace cleancoderscom.socketserver
{

	public interface SocketService
	{
        //Java Socket replaced with .NET TcpClient
        void serve(TcpClient s);
	}

}