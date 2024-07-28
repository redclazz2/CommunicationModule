using System.Net.Sockets;
using Server.Data;
using Server.Interface;
using Shared.Interface;

namespace Server.Communicator
{
    public class ServerCommunicator : IServerCommunicator
    {
        private readonly IMediator mediator;
        private readonly ISocketServerCommunicator serverSocket;
        private readonly Dictionary<int, ISocketSessionCommunicator> clients = [];

        public ServerCommunicator(IMediator mediator, int port, ProtocolType protocolType)
        {
            this.mediator = mediator;

            serverSocket = new SocketServerCommunicator(
                port,
                protocolType
            );
        }

        public void Init()
        {
            System.Console.WriteLine("Initializaing server socket ...");

            if (serverSocket.Init())
            {
                System.Console.WriteLine("Success.");
                Listen();
            }
            else
            {
                System.Console.WriteLine("Error: Couldn't initialize server socket.");
            }
        }

        public bool Close()
        {
            System.Console.WriteLine("Closing server socket ...");
            return serverSocket.Close();
        }

        public void Listen()
        {
            System.Console.WriteLine($"Listening ...");
            //In the future a separated thread can be instantiated here.

            var session = serverSocket.Listen();
            clients.Add(session.sessionId,session);
            Read(session);
        }

        public void Read(ISocketSessionCommunicator session)
        {
            var data = session.Read().Result;
            mediator.Notify(MessageType.Communicator, data);
        }

        public bool Write(MessageData data)
        {
            try
            {
                var session = clients.GetValueOrDefault(data.SessionId);
                session?.Write(data);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}