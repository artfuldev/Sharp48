using System;
using System.IO;
using System.Threading.Tasks;
using Sharp48.Solvers;
using Sharp48.UserInterfaces;
using Sharp48.Core.Moves;

namespace Sharp48.ConsoleApp
{
    public class Program
    {
        private static readonly ISolver Solver = new RandomSolver();
        public static void Main(string[] args)
        {
            var driverPath = Path.Combine(Environment.CurrentDirectory);
            using (var ui = new GoogleChromeUI(driverPath))
            {
                ui.Initialize();
                var game = ui.Game;
                while (!game.Over)
                {
                    var move = Solver.GetBestMove(game);
                    game = ui.MakeMove(move);
                }
                Console.ReadLine();
            }
        }
    }
}