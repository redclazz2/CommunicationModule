namespace CommunicationShared.Data
{
    public class ReadData(byte[] data, int count)
    {
        public int Count {get; set;} = count;
        public byte[] Data {get; set;} = data;
    }
}