using System.Net.Sockets;
using Server.Data;
using Server.Interface;

namespace Server.Domain.Base
{
    public abstract class BaseSocketSessionCommunicator : ISocketSessionCommunicator
    {
        public int sessionId;
        public Socket socket;

        public BaseSocketSessionCommunicator(Socket socket, int sessionId){
            this.socket = socket;
            this.sessionId = sessionId;
        }
        
        public abstract bool Close();

        public abstract Task<MessageData> Read();

        public abstract void Write(MessageData data);
    }
}