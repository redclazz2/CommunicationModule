using System.Net.Sockets;
using Server.Interface;
using Server.Data;

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

        public abstract Task<Request> Read();

        public abstract void Write(Response data);
    }
}