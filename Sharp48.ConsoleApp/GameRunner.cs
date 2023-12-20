using System;
using System.Threading.Tasks;
using Sharp48.Solvers;
using Sharp48.UserInterfaces;

namespace Sharp48.ConsoleApp
{
    /// <summary>
    ///     A disposable wrapper that handles running the game. It takes a solver and a UI, queries the UI for the game and the
    ///     solver for the best moves and plays them on the UI until the game is over.
    /// </summary>
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
            Console.ReadLine();
        }
    }
}