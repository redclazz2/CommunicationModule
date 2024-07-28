namespace Client.Interface
{
    public interface IClientCommunicator
    {
        public bool Connect();
        public Object Read();
        public void Write();
        public void Close();
    }
}