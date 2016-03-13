using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Core.PlayArea;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class PotentialMergesEvaluator : ICacheableEvaluator
    {
        private static double MergeScore(IEnumerable<ISquare> squares)
        {
            var array = squares.Select(x => x.GetSafeTileValue()).ToArray();
            var score = 0d;
            for(var i=0;i<array.Length-1;i++)
                if (array[i] == array[i + 1])
                    score += array[i];
            return score;
        }

        public double Evaluate(IGame game)
            => game.Grid.Columns.Aggregate(
                game.Grid.Rows.Aggregate(0d, (current, next) => current + MergeScore(next)),
                (current, next) => current + MergeScore(next));

        public string GetCacheKey(IGame game) => game.Grid.ToString();
    }
}