using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Sharp48.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var driverPath = Path.Combine(Environment.CurrentDirectory);
            var options = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(driverPath, options);

            driver.Navigate().GoToUrl("https://gabrielecirulli.github.io/2048/");

            driver.Quit();

            Console.ReadLine();
        }
    }
}
