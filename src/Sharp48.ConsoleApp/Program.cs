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
                    new ExpectimaxEvaluator(new CachingEvaluator(new SumEvaluator(new List<IEvaluator>
                    {
                        new TransformEvaluator(new EmptyTileEvaluator(), score => score),
                        new TransformEvaluator(new MergeEvaluator(), (score)=>score)
                        //new TransformEvaluator(new SmoothnessEvaluator(), (score) => score),
                        //new TransformEvaluator(new MonotonicityEvaluator(), (score) => score),
                        //new TransformEvaluator(new SnakeEvaluator(), (score) => score)
                    }))));
            var ui = new GoogleChromeUI(Path.Combine(Environment.CurrentDirectory));
            using (var runner = new GameRunner(ui, solver))
                runner.Run();
        }
    }
}