using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.InterfaceAdapter
{
    public class Adapter : IAdapter
    {
        private readonly IWebDriver _driver;
        public Adapter(string driverPath):this(new ChromeDriver(driverPath, new ChromeOptions())) { }
        public Adapter(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Initialize()
        {
            _driver.Navigate().GoToUrl("https://gabrielecirulli.github.io/2048/");
            var waiter = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            waiter.Until(d => d.Title == "2048");
        }

        // TODO: Get Grid from UI
        public IGrid GetGrid(Move move = Move.None)
        {
            if (move != Move.None)
                MakeMove(move);
            var elements =
                _driver.FindElements(By.CssSelector(".tile-container .tile")).Select(x => x.GetAttribute("class"));
            throw new NotImplementedException();
        }

        // TODO: Make move
        private void MakeMove(Move move)
        {
            
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}