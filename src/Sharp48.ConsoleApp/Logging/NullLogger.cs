namespace Sharp48.ConsoleApp.Logging
{
    public class NullLogger : ILogger
    {
        public void Log(object value) { }
    }
}