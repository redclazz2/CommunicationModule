using System.Net;
using System.Net.Sockets;
using CommunicationServer.Interface;
using CommunicationShared.Helper;

namespace CommunicationServer.Communicator
{
    public class SocketServer
    {
        readonly IPEndPoint ipEndpoint;
        readonly Socket socket;

        public SocketServer(int port, ProtocolType protocolType)
        {
            IPAddress[] ips = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            ipEndpoint = new IPEndPoint(
                ips[0],
                port
            );

            socket = new Socket(
                ipEndpoint.AddressFamily,
                SocketType.Stream,
                protocolType
            );
        }

        public bool Init()
        {
            try
            {
                socket.Bind(ipEndpoint);
                socket.Listen();
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                socket.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Socket Listen()
        {
            var helper = socket.Accept();
            return helper;
        }
    }
}