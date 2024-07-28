using Server.Data;

namespace Server.Interface
{
    public interface IServerCommunicator
    {
        public bool Init();
        public void Listen();
        public void Close();
        public void Read(ISocketSessionCommunicator session);
        public bool Write(MessageData data);
    }
}