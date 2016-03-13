using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Core.PlayArea;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public abstract class RowEvaluatorBase : IRowEvaluator
    {
        private readonly IDictionary<string, double> _hashTable = new Dictionary<string, double>();
        public double Evaluate(IGame game)
            => game.Grid.Columns.Aggregate(
                game.Grid.Rows.Aggregate(0d, (current, next) => current + Evaluate(next)),
                (current, next) => current + Evaluate(next));

        public string GetCacheKey(IGame game) => game.Grid.ToString();

        public double Evaluate(IEnumerable<ISquare> squares)
        {
            var tiles = squares.Select(x => x.GetSafeTileValue()).ToArray();
            var key = string.Join(",", tiles);
            return _hashTable.ContainsKey(key) ? _hashTable[key] : (_hashTable[key] = EvaluateImplementation(tiles));
        }

        protected abstract double EvaluateImplementation(uint[] tiles);
    }
}