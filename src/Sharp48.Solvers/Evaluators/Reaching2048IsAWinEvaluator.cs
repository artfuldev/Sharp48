using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class Reaching2048IsAWinEvaluator : ICacheableEvaluator
    {
        public double Evaluate(IGame game)
            => game.Grid.Squares.Any(x => x.GetSafeTileValue() == 2048) ? double.PositiveInfinity : 0;

        public string GetCacheKey(IGame game) => game.Grid.ToString();
    }
}