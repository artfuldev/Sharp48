using System;
using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public class TransformEvaluator : ICacheableEvaluator
    {
        private readonly IEvaluator _evaluator;
        private readonly Func<double, double> _transformFunc;

        public TransformEvaluator(IEvaluator evaluator, Func<double, double> transformFunc)
        {
            _evaluator = evaluator;
            _transformFunc = transformFunc;
        }

        public double Evaluate(IGame game) => _transformFunc(_evaluator.Evaluate(game));

        public string GetCacheKey(IGame game) => game.Grid.ToString();
    }
}