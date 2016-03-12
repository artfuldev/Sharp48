using System.Collections.Generic;
using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public class CachingEvaluator : IEvaluator
    {
        private readonly ICacheableEvaluator _evaluator;
        private readonly IDictionary<string, double> _hashTable = new Dictionary<string, double>();

        public CachingEvaluator(ICacheableEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public double Evaluate(IGame game)
        {
            var key = _evaluator.GetCacheKey(game);
            if (!_hashTable.ContainsKey(key))
                _hashTable[key] = _evaluator.Evaluate(game);
            return _hashTable[key];
        }
    }
}