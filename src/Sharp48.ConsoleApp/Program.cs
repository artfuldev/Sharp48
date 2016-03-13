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
                new TransformEvaluator(sumEvaluator, (score) => 11*score),
                new TransformEvaluator(emptyTileEvaluator, (score) => 270*score),
                new TransformEvaluator(mergeEvaluator, (score) => 700*score),
                new TransformEvaluator(monotonicityEvaluator, (score) => 47*score),
                new Reaching2048IsAWinEvaluator(),
            };
            var mainEvaluator = new ExpectimaxEvaluator(new CachingEvaluator(new AggregateEvaluator(evaluators)), 4, 0.0001);
            var solver = new IntelligentSolver(mainEvaluator);
            var driverPath = Path.Combine(Environment.CurrentDirectory);
            var ui = new GoogleChromeUI(driverPath);
            var logger = new NullLogger();
            using (var runner = new GameRunner(ui, solver, logger))
                runner.Run();
        }
    }
}