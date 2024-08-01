namespace CommunicationShared.Interface
{
    public interface IMediator
    {
        public void Notify(MessageType message, Object data);
    }

    public enum MessageType{
        Communicator
    }
}