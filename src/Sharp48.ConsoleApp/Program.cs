using System;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Sharp48.InterfaceAdapter;

namespace Sharp48.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var driverPath = Path.Combine(Environment.CurrentDirectory);
            using (var adapter = new Adapter(driverPath))
            {
                adapter.Initialize();
                Console.ReadLine();
            }
        }
    }
}
