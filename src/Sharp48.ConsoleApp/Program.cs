using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sharp48.Solvers;
using Sharp48.Solvers.Evaluators;
using Sharp48.Solvers.Evaluators.RowEvaluation;
using Sharp48.UserInterfaces;

namespace Sharp48.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var solver =
                new IntelligentSolver(
                    new ExpectimaxEvaluator(new CachingEvaluator(new SumEvaluator(new List<IEvaluator>
                    {
                        new TransformEvaluator(new EmptyTileEvaluator(),
                            (score, game) => score*game.Grid.Squares.Max(x => x.Tile?.Value ?? 0)),
                        //new TransformEvaluator(new MergeEvaluator(), (score, game) => score*4),
                        //new TransformEvaluator(new MergesAwayEvaluator(), (score, game) => score*2),
                        //new Reaching2048IsAWinEvaluator(),
                    })), 4));
            var ui = new GoogleChromeUI(Path.Combine(Environment.CurrentDirectory));
            using (var runner = new GameRunner(ui, solver))
                runner.Run();
        }
    }
}