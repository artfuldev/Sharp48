using System.Collections.Concurrent;
using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public class CachingEvaluator : IEvaluator
    {
        private readonly IEvaluator _evaluator;
        private readonly IDictionary<string, double> _hashTable = new ConcurrentDictionary<string, double>();
        private readonly int _maxEntries;

        public CachingEvaluator(IEvaluator evaluator, int maxEntries)
        {
            _evaluator = evaluator;
            _maxEntries = maxEntries;
        }

        public double Evaluate(IGame game)
        {
            if (_hashTable.Count >= _maxEntries)
                _hashTable.Clear();
            var key = game.Grid.ToString();
            if (!_hashTable.ContainsKey(key))
                _hashTable[key] = _evaluator.Evaluate(game);
            return _hashTable[key];
        }
    }
}