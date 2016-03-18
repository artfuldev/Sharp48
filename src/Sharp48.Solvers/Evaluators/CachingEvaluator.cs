using System.Collections.Concurrent;
using System.Collections.Generic;
using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public class CachingEvaluator : IEvaluator
    {
        private readonly IEvaluator _evaluator;
        private readonly IDictionary<string, double> _hashTable = new ConcurrentDictionary<string, double>();

        public CachingEvaluator(IEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public double Evaluate(IGame game)
        {
            var key = game.Grid.ToString();
            if (!_hashTable.ContainsKey(key))
                _hashTable[key] = _evaluator.Evaluate(game);
            return _hashTable[key];
        }
    }
}