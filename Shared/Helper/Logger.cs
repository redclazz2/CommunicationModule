namespace CommunicationShared.Helper
{
    public enum LoggerLevel{
        Info,
        Warning,
        Error,
    }

    public static class Logger
    {
        public static void Log(LoggerLevel level, string message){
            System.Console.WriteLine($"--- {level}: {message}. ---");
        }
    }
}