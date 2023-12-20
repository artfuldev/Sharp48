using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Sharp48.Core;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Sharp48.UserInterfaces
{
    public class GoogleChromeUI : IUserInterface
    {
        private IWebDriver _driver;

        public void Initialize()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://gabrielecirulli.github.io/2048/");
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title == "2048");
            var waitForCookies = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.CssSelector("#ez-accept-all")));
            _driver.FindElement(By.CssSelector("#ez-accept-all")).Click();
            _driver.ExecuteJavaScript<string>(@"
            window._func_tmp = GameManager.prototype.isGameTerminated;
            GameManager.prototype.isGameTerminated = function() {
                GameManager._instance = this;
                return true;
            };");
            _driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Up);
            _driver.ExecuteJavaScript<string>(@"GameManager.prototype.isGameTerminated = window._func_tmp;");
        }

        public IGame Game
        {
            get
            {
                var json = _driver.ExecuteJavaScript<string>(@"return JSON.stringify(GameManager._instance.grid)");
                var gameManagerGrid = JsonConvert.DeserializeObject<GameManagerGrid>(json);
                gameManagerGrid.Cells = gameManagerGrid.Cells.Transpose().ToList();
                var gridString =
                    gameManagerGrid.Cells.Aggregate("",
                        (seed, row) => row.Aggregate(seed, (current, cell) => current + cell?.Value + ","));
                var grid = Grid.Parse(gridString.Remove(gridString.Length - 1));
                var gameOver = _driver.ExecuteJavaScript<bool>(@"return GameManager._instance.over");
                return new Game(grid, gameOver);
            }
        }

        public IGame MakeMove(Move move)
        {
            _driver.ExecuteJavaScript<string>($"GameManager._instance.move({(byte) move})");
            return Game;
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        private class GameManagerGrid
        {
            public List<List<Cell>> Cells { get; set; }
        }

        private class Cell
        {
            public string Value { get; set; }
        }
    }
}