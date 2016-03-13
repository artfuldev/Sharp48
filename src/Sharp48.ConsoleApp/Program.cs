using System;
using System.Collections.Generic;
using System.IO;
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
            var driverPath = Path.Combine(Environment.CurrentDirectory);
            var ui = new GoogleChromeUI(driverPath);
            var logger = new NullLogger();
            var evaluators = new List<IEvaluator>()
            {
                new TransformEvaluator(new RowScoreEvaluator(3.5), (score) => 11*score),
                new TransformEvaluator(new EmptyTileEvaluator(), (score) => 270*score),
                new TransformEvaluator(new MergeEvaluator(), (score) => 700*score),
                new TransformEvaluator(new MonotonicityEvaluator(4), (score) => 47*score),
                new Reaching2048IsAWinEvaluator(),
            };
            var mainEvaluator = new ExpectimaxEvaluator(new CachingEvaluator(new AggregateEvaluator(evaluators)), 4);
            var solver = new IntelligentSolver(mainEvaluator);
            using (var runner = new GameRunner(ui, solver, logger))
                runner.Run();
        }
    }
}