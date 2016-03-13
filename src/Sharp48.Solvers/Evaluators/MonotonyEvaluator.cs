using System;
using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Core.PlayArea;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class MonotonyEvaluator : ICacheableEvaluator
    {
        private readonly IDictionary<string, double> _squaresScores = new Dictionary<string, double>();

        private static bool IsMonotonic(IEnumerable<uint> tiles)
        {
            var array = tiles as uint[] ?? tiles.ToArray();
            for (var i = 0; i < array.Length - 1; i++)
                if (array[i + 1] < array[i])
                    return false;
            return true;
        }

        private double Evaluate(IEnumerable<ISquare> squares)
        {
            var array = squares.Select(x => x.GetSafeTileValue()).ToArray();
            var key = string.Join(",", array);
            if (_squaresScores.ContainsKey(key))
                return _squaresScores[key];
            var score = IsMonotonic(array) || IsMonotonic(array.Reverse()) ? array.Max() + array.Min() : 0;
            return _squaresScores[key] = score;
        }

        public double Evaluate(IGame game)
        {
            return game.Grid.Columns.Aggregate(
                game.Grid.Rows.Aggregate(0d, (current, next) => current + Evaluate(next)),
                (current, next) => current + Evaluate(next));
        }

        public string GetCacheKey(IGame game) => game.Grid.ToString();
    }
}