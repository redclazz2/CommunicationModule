using Server.Data;

namespace Server.Interface
{
    public interface ISocketSessionCommunicator
    {
        public Task<Request> Read();
        public void Write(Response data);
        public bool Close();
    }
}