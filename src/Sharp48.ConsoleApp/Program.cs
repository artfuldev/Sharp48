using System;
using System.IO;
using Sharp48.UserInterfaces;

namespace Sharp48.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var driverPath = Path.Combine(Environment.CurrentDirectory);
            using (var adapter = new GoogleChromeUserInterface(driverPath))
            {
                adapter.Initialize();
                Console.ReadLine();
            }
        }
    }
}