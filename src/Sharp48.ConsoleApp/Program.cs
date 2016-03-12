using System;
using System.Collections.Generic;
using System.IO;
using Sharp48.ConsoleApp.Logging;
using Sharp48.Solvers;
using Sharp48.Solvers.Evaluators;
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
            var evaluators = new List<IEvaluator>() {new ScoreEvaluator(), new SmoothnessEvaluator(), new FreeSpaceEvaluator()};
            var mainEvaluator = new ExpectimaxEvaluator(new CachingEvaluator(new AggregateEvaluator(evaluators)));
            var solver = new IntelligentSolver(mainEvaluator);
            using (var runner = new GameRunner(ui, solver, logger))
                runner.Run();
        }
    }
}