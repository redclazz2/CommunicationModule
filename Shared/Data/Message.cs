namespace Shared.Data
{
    public class Message<T>(int Command, T Data)
    {
        public int Command{get; set;} = Command;
        public T Data {get; set;} = Data;
    }
}