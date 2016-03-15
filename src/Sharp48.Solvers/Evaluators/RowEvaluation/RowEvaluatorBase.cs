using System.Collections.Generic;
using System.Linq;
using Facet.Combinatorics;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public abstract class RowEvaluatorBase : IRowEvaluator
    {
        private readonly IDictionary<string, double> _hashTable = new Dictionary<string, double>();

        public double Evaluate(ulong grid)
            =>
                grid.GetColumns()
                    .Aggregate(grid.GetRows().Aggregate(0d, (current, next) => current + Evaluate(next.AsTiles())),
                        (current, next) => current + Evaluate(next.AsTiles()));

        protected RowEvaluatorBase()
        {
            var values = Enumerable.Range(0, 15).Select(x => (byte) x).ToArray();
            var rows = new Variations<byte>(values, 4, GenerateOption.WithRepetition);
            foreach (var row in rows)
                Evaluate(row.ToArray());
        }

        public double Evaluate(byte[] tiles)
        {
            var key = string.Join(",", tiles);
            return _hashTable.ContainsKey(key) ? _hashTable[key] : (_hashTable[key] = EvaluateImplementation(tiles));
        }

        protected abstract double EvaluateImplementation(byte[] tiles);
    }
}