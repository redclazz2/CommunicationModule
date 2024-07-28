using Server.Data;

namespace Server.Interface
{
    public interface ISocketSessionCommunicator
    {
        public Task<MessageData> Read();
        public void Write(MessageData data);
        public bool Close();
    }
}