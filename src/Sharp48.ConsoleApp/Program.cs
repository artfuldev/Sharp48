using System;
using System.Collections.Generic;
using System.IO;
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
                    new AlphaBetaEvaluator(new CachingEvaluator(new SumEvaluator(new List<IEvaluator>()
                    {
                        new TransformEvaluator(new TileSumEvaluator(3.5), (score) => 200000 - 11*score),
                        new TransformEvaluator(new EmptyTileEvaluator(), (score) => 270*score),
                        new TransformEvaluator(new MergeEvaluator(), (score) => 700*score),
                        new TransformEvaluator(new MonotonicityEvaluator(4), (score) => 47*score)
                    })), 3));
            var ui = new GoogleChromeUI(Path.Combine(Environment.CurrentDirectory));
            using (var runner = new GameRunner(ui, solver))
                runner.Run();
        }
    }
}