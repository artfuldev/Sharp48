using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public class TransformEvaluator : IEvaluator
    {
        private readonly IEvaluator _evaluator;
        private readonly Func<double, IGame, double> _transformFunc;

        public TransformEvaluator(IEvaluator evaluator, Func<double, IGame, double> transformFunc)
        {
            _evaluator = evaluator;
            _transformFunc = transformFunc;
        }

        public double Evaluate(IGame game) => _transformFunc(_evaluator.Evaluate(game), game);
    }
}