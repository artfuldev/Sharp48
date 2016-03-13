using System.Collections.Generic;

namespace Sharp48.Solvers.Evaluators
{
    public class CachingEvaluator : IEvaluator
    {
        private readonly IEvaluator _evaluator;
        private readonly IDictionary<ulong, double> _hashTable = new Dictionary<ulong, double>();

        public CachingEvaluator(IEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public double Evaluate(ulong grid)
        {
            if (!_hashTable.ContainsKey(grid))
                _hashTable[grid] = _evaluator.Evaluate(grid);
            return _hashTable[grid];
        }
    }
}