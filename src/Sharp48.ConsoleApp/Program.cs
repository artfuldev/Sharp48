using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sharp48.ConsoleApp.Logging;
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
            var sumEvaluator = new SumEvaluator(3.5);
            var emptyTileEvaluator = new EmptyTileEvaluator();
            var mergeEvaluator = new MergeEvaluator();
            var monotonicityEvaluator = new MonotonicityEvaluator(4);
            sumEvaluator.Preload();
            emptyTileEvaluator.Preload();
            mergeEvaluator.Preload();
            monotonicityEvaluator.Preload();
            var evaluators = new List<IEvaluator>()
            {
                new TransformEvaluator(sumEvaluator, (score) => 20000 -11*score),
                new TransformEvaluator(emptyTileEvaluator, (score) => 270*score),
                new TransformEvaluator(mergeEvaluator, (score) => 700*score),
                new TransformEvaluator(monotonicityEvaluator, (score) => -47*score),
                new Reaching2048IsAWinEvaluator(),
            };
            var mainEvaluator = new ExpectimaxEvaluator(new CachingEvaluator(new AggregateEvaluator(evaluators)), 3, 0.0001);
            var logger = new ConsoleLogger();
            var solver = new IntelligentSolver(mainEvaluator);
            var ui = new GoogleChromeUI(Path.Combine(Environment.CurrentDirectory));
            using (var runner = new GameRunner(ui, solver, logger))
                runner.Run();
        }
    }
}