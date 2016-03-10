using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Sharp48.Core;
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
                var grid = Grid.ParseGrid(gridString.Remove(gridString.Length - 1));
                var gameOver = _driver.ExecuteJavaScript<bool>(@"return GameManager._instance.over");
                var score = _driver.ExecuteJavaScript<long>(@"return GameManager._instance.score");
                return new Game(grid, gameOver, score);
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