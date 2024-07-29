using System.Text;
using System.Text.Json;

namespace Shared.Middleware
{
    public static class Formatter
    {
        private static readonly string EOM = "<EOM>";
        public static byte[] Serialize<T>(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Object is null.");
            }

            byte[] toSend = JsonSerializer.SerializeToUtf8Bytes(data);

            return toSend;
        }

        public static byte[] AddDelimiter(byte[] send)
        {
            byte[] delimiter = Encoding.UTF8.GetBytes(EOM);
            byte[] finalSend = new byte[send.Length + delimiter.Length];
            Buffer.BlockCopy(send, 0, finalSend, 0, send.Length);
            Buffer.BlockCopy(delimiter, 0, finalSend, send.Length, delimiter.Length);

            return finalSend;
        }

        public static T Deserialize<T>(string data)
        {
            T? result = JsonSerializer.Deserialize<T>(data);

            if (result == null)
            {
                throw new InvalidOperationException("Null object after deserialization");
            }

            return result;
        }

        public static string RemoveDelimiter(byte[] data, int count){
            var recievedString = Encoding.UTF8.GetString(data, 0, count);
            return recievedString.Remove(recievedString.Length - EOM.Length);
        }
    }
}