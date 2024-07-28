namespace Client.Interface
{
    public interface ISocketClientCommunicator
    {
        public Task<bool> Connect();
        public bool Close();
        public Task<object> Read();
        public void Write(object data);
    }
}