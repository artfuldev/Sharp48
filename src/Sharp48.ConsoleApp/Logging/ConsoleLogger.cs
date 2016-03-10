using System;

namespace Sharp48.ConsoleApp.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(object value) => Console.WriteLine(value);
    }
}