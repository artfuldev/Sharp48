using System;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
            var waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            waiter.Until(d => d.Title == "2048");
            var elements =
                driver.FindElements(By.CssSelector(".tile-container .tile")).Select(x => x.GetAttribute("class"));

            driver.Quit();
        }
    }
}
