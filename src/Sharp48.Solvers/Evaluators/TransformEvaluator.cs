using System;

namespace Sharp48.Solvers.Evaluators
{
    public class TransformEvaluator : IEvaluator
    {
        private readonly IEvaluator _evaluator;
        private readonly Func<double, double> _transformFunc;

        public TransformEvaluator(IEvaluator evaluator, Func<double, double> transformFunc)
        {
            _evaluator = evaluator;
            _transformFunc = transformFunc;
        }

        public double Evaluate(ulong grid) => _transformFunc(_evaluator.Evaluate(grid));
    }
}