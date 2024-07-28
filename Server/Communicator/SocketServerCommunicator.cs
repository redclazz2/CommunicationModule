using System.Net;
using System.Net.Sockets;
using Server.Domain.Base;
using Server.Interface;

namespace Server.Communicator
{
    public class SocketServerCommunicator : ISocketServerCommunicator
    {
        IPEndPoint ipEndpoint;
        Socket socket;
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
                ProtocolType.Tcp
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
            while (true)
            {
                var helper = socket.Accept();
                System.Console.WriteLine("Client found ...");

                var session = new SocketSessionCommunicator(helper, sessionCounter);
                sessionCounter++;
                return session;
            }
        }
    }
}