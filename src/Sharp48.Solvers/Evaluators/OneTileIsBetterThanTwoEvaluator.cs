using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class OneTileIsBetterThanTwoEvaluator : ICacheableEvaluator
    {
        public double Evaluate(IGame game)
            => game.Grid.Squares.Select(x => x.GetSafeTileValue())
                .Aggregate(0d, (current, value) => current + value * value);

        public string GetCacheKey(IGame game) => game.Grid.ToString();
    }
}