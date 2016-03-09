using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.UserInterfaces
{
    public class GoogleChromeUI : IUserInterface
    {
        private readonly IWebDriver _driver;

        public GoogleChromeUI(string driverPath) : this(new ChromeDriver(driverPath, new ChromeOptions()))
        {
        }

        private GoogleChromeUI(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Initialize()
        {
            _driver.Navigate().GoToUrl("https://gabrielecirulli.github.io/2048/");
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title == "2048");
            _driver.ExecuteJavaScript<string>(@"
            window._func_tmp = GameManager.prototype.isGameTerminated;
            GameManager.prototype.isGameTerminated = function() {
                GameManager._instance = this;
                return true;
            };");
            _driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Up);
            _driver.ExecuteJavaScript<string>(@"GameManager.prototype.isGameTerminated = window._func_tmp;");
        }

        public IGrid GetGrid(Move? move = null)
        {
            if (move != null)
                MakeMove(move.Value);
            var json = _driver.ExecuteJavaScript<string>(@"window.JSON.stringify(GameManager._instance.grid)");
            var gameManagerGrid = JsonConvert.DeserializeObject<GameManagerGrid>(json);
            var gridString =
                gameManagerGrid.Cells.Aggregate("",
                    (current1, row) => row.Aggregate(current1, (current, cell) => current + (cell?.Value + ",")))
                    .TrimEnd(',');
            return Grid.ParseGrid(gridString);
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        // TODO: Make move
        private void MakeMove(Move move)
        {
            _driver.ExecuteJavaScript<string>($"GameManager._instance.move({((byte) move)})");
        }

        private class GameManagerGrid
        {
            public List<Cell[]> Cells { get; set; }
        }

        private class Cell
        {
            public string Value { get; set; }
        }
    }
}