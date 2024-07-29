using System.Net;
using System.Net.Sockets;
using Server.Domain.Base;
using Server.Interface;
using Shared.Helper;

namespace Server.Communicator
{
    public class SocketServerCommunicator : ISocketServerCommunicator
    {
        readonly IPEndPoint ipEndpoint;
        readonly Socket socket;
        int sessionCounter = 0;

        public SocketServerCommunicator(int port, ProtocolType protocolType)
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

        public BaseSocketSessionCommunicator Listen()
        {
            var helper = socket.Accept();
            Logger.Log(LoggerLevel.Info, "Client Found");

            var session = new SocketSessionCommunicator(helper, sessionCounter);
            sessionCounter++;
            return session;
        }
    }
}