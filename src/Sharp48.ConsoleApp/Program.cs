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
                    new ExpectimaxEvaluator(new SumEvaluator(new List<IEvaluator>()
                    {
                        //new TransformEvaluator(new TileSumEvaluator(5), (score) => score),
                        new TransformEvaluator(new EmptyTileEvaluator(), (score) => Math.Pow(score, 15)),
                        new TransformEvaluator(new MergeEvaluator(), (score) => score),
                        new TransformEvaluator(new MonotonicityEvaluator(14), (score) => score)
                    }), 4, 0.01));
            var ui = new GoogleChromeUI(Path.Combine(Environment.CurrentDirectory));
            using (var runner = new GameRunner(ui, solver))
                runner.Run();
        }
    }
}