using System;
using System.IO;
using Sharp48.Solvers;
using Sharp48.UserInterfaces;
using Sharp48.Core.Moves;

namespace Sharp48.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var driverPath = Path.Combine(Environment.CurrentDirectory);
            using (var ui = new GoogleChromeUI(driverPath))
            {
                ui.Initialize();
                var grid = ui.GetGrid();
                var solver = new RandomSolver();
                var move = solver.GetBestMove(grid);
                while (move != null)
                {
                    grid = ui.GetGrid(move);
                    move = solver.GetBestMove(grid);
                }
                Console.ReadLine();
            }
        }
    }
}