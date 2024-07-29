using Server.Data;

namespace Server.Interface
{
    public interface IServerCommunicator
    {
        public bool Init();
        public void Listen();
        public void Close();

        //TODO: Bound to dissapear when clients are separated in threads.
        public void Read(int sessionId);
        public void Write(Response data);
    }
}