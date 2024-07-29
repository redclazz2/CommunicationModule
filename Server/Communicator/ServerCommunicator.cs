using Server.Interface;
using Server.Data;
using Shared.Interface;
using Shared.Helper;
using System.Net.Sockets;

namespace Server.Communicator
{
    public class ServerCommunicator : IServerCommunicator
    {
        private readonly IMediator mediator;
        private readonly SocketServer serverSocket;
        private readonly Dictionary<int, SocketSession> clients = [];
        int sessionCounter = 0;

        public ServerCommunicator(IMediator mediator)
        {
            this.mediator = mediator;
            //TODO: Read Port and Protocol from configuration file
            serverSocket = new SocketServer(
                8056,
                System.Net.Sockets.ProtocolType.Tcp
            );
        }

        public bool Init()
        {
            Logger.Log(LoggerLevel.Info, "Initializing Communication Module");
            var init = serverSocket.Init();

            if (init)
            {
                Logger.Log(LoggerLevel.Info, "Success");
            }
            else
            {
                Logger.Log(LoggerLevel.Error, "Failed to Initialize Communication Module.");
            }

            return init;
        }

        public void Close()
        {
            Logger.Log(LoggerLevel.Info, "Closing server socket");
            if (serverSocket.Close())
            {
                foreach (SocketSession c in clients.Values.Cast<SocketSession>())
                {
                    if (!c.Close())
                    {
                        Logger.Log(LoggerLevel.Warning, $"Error while closing socket for client {c.sessionId}");
                    }
                }
                clients.Clear();
                Logger.Log(LoggerLevel.Info, "Bye");
            }
            else
            {
                Logger.Log(LoggerLevel.Error, "Error on communication module clean up");
            }
        }

        public void Listen()
        {
            while (true)
            {
                Logger.Log(LoggerLevel.Info, "Listening");

                //In the future a separated thread can be instantiated here.
                Socket sessionSocket = serverSocket.Listen();
                Logger.Log(LoggerLevel.Info, $"Client Found. Id assigned: {sessionCounter}");
                SocketSession session = new(
                    sessionSocket,
                    sessionCounter
                );

                sessionCounter++;
                clients.Add(session.sessionId, session);
                Read(session.sessionId);
            }
        }

        public void Read(int sessionId)
        {
            var session = clients.GetValueOrDefault(sessionId);
            var data = session?.Read().Result;

            if (data != null)
            {
                mediator.Notify(MessageType.Communicator, data);
            }
        }

        public void Write(Response data)
        {
            var session = clients.GetValueOrDefault(data.SessionId);
            session?.Write(data);
        }
    }
}