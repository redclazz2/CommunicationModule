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

        public static T Deserialize<T>(byte[] data, int count)
        {
            var sData = Encoding.UTF8.GetString(data,0,count);
            T? result = JsonSerializer.Deserialize<T>(sData);

            if (result == null)
            {
                throw new InvalidOperationException("Null object after deserialization");
            }

            return result;
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
    }
}