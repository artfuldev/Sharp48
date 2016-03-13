using System;
using System.Collections.Generic;
using System.Linq;
using Facet.Combinatorics;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public abstract class RowEvaluatorBase : IRowEvaluator
    {
        private readonly IDictionary<string, double> _hashTable = new Dictionary<string, double>();

        public double Evaluate(IGame game)
            => game.Grid.Columns.Aggregate(
                game.Grid.Rows.Aggregate(0d,
                    (current, next) => current + Evaluate(next.Select(x => x.GetSafeTileValue()).ToArray())),
                (current, next) => current + Evaluate(next.Select(x => x.GetSafeTileValue()).ToArray()));

        public string GetCacheKey(IGame game) => game.Grid.ToString();

        public void Preload()
        {
            var values = Enumerable.Range(0, 16).Select(x => x == 0 ? 0 : (uint) Math.Pow(2, x)).ToArray();
            var rows = new Variations<uint>(values, 4, GenerateOption.WithRepetition);
            foreach (var row in rows)
                Evaluate(row.ToArray());
        }

        public double Evaluate(uint[] tiles)
        {
            var key = string.Join(",", tiles);
            return _hashTable.ContainsKey(key) ? _hashTable[key] : (_hashTable[key] = EvaluateImplementation(tiles));
        }

        protected abstract double EvaluateImplementation(uint[] tiles);
    }
}