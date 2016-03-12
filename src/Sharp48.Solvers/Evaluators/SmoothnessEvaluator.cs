using System;
using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Core.PlayArea;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class SmoothnessEvaluator : ICacheableEvaluator
    {
        private readonly IDictionary<string, double> _squaresScores = new Dictionary<string, double>();

        private double Evaluate(IEnumerable<ISquare> squares)
        {
            var array = squares.Select(x=>x.GetSafeTileValue()).ToArray();
            var key = string.Join(",", array);
            if (_squaresScores.ContainsKey(key))
                return _squaresScores[key];
            var score = 0d;
            for (var i = 0; i < array.Length - 1; i++)
            {
                if (i != 0)
                    score -= Math.Abs(array[i - 1] - array[i]);
                if (i != array.Length - 1)
                    score -= Math.Abs(array[i + 1] - array[i]);
            }
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