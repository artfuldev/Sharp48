using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public class SumEvaluator : IEvaluator
    {
        private readonly IEnumerable<IEvaluator> _evaluators;

        public SumEvaluator(IEnumerable<IEvaluator> evaluators)
        {
            _evaluators = evaluators;
        }

        public double Evaluate(IGame game)
        {
            double sum = 0;
            Parallel.ForEach(_evaluators, evaluator =>
            {
                double localEvaluation = evaluator.Evaluate(game);
                lock (_evaluators)
                {
                    sum += localEvaluation;
                }
            });
            return sum;
        }
    }
}