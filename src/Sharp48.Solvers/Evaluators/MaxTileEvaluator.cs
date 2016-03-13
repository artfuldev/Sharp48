using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class MaxTileEvaluator:ICacheableEvaluator
    {
        public double Evaluate(IGame game) => game.Grid.Squares.Max(x => x.GetSafeTileValue());

        public string GetCacheKey(IGame game) => game.Grid.ToString();
    }
}