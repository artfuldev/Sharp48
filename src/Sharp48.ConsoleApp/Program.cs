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
                        new TransformEvaluator(new TileSumEvaluator(2), (score) => score),
                        new TransformEvaluator(new EmptyTileEvaluator(), (score) => 32768*score),
                        new TransformEvaluator(new MergeEvaluator(), (score) => Math.Pow(2, score)),
                        new TransformEvaluator(new MonotonicityEvaluator(2), (score) => score),
                        new TransformEvaluator(new SmoothnessEvaluator(2), (score) => score)
                    })));
            var ui = new GoogleChromeUI(Path.Combine(Environment.CurrentDirectory));
            using (var runner = new GameRunner(ui, solver))
                runner.Run();
        }
    }
}