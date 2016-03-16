using System;
using System.Threading.Tasks;
using Sharp48.ConsoleApp.Logging;
using Sharp48.Solvers;
using Sharp48.UserInterfaces;

namespace Sharp48.ConsoleApp
{
    public class GameRunner : IDisposable
    {
        private readonly ISolver _solver;
        private readonly IUserInterface _ui;

        public GameRunner(IUserInterface ui, ISolver solver)
        {
            _ui = ui;
            _solver = solver;
        }

        public void Dispose()
        {
            _ui.Dispose();
        }

        public void Run()
        {
            _ui.Initialize();
            var game = _ui.Game;
            while (!game.Over)
            {
                game = _ui.MakeMove(_solver.GetBestMove(game));
            }
            Task.Delay(3000).Wait();
        }
    }
}