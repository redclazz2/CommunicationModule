using Server.Domain.Base;

namespace Server.Interface
{
    public interface ISocketServerCommunicator
    {
        public bool Init();
        public bool Close();
        public BaseSocketSessionCommunicator Listen();
    }
}