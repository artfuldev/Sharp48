using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public class AggregateEvaluator : ICacheableEvaluator
    {
        private readonly IEnumerable<IEvaluator> _evaluators;

        public AggregateEvaluator(IEnumerable<IEvaluator> evaluators)
        {
            _evaluators = evaluators;
        }

        public double Evaluate(IGame game)
            => _evaluators.Aggregate(0D, (current, next) => current + next.Evaluate(game));

        public string GetCacheKey(IGame game) => game.Grid.ToString();
    }
}