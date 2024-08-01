using Shared.Data;

namespace Client.Interface
{
    public interface IClientCommunicator
    {
        public bool Connect();
        public void Read();
        public void Write(Message<object> message);
        public void Close();
    }
}