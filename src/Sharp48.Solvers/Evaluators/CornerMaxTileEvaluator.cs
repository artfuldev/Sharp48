using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class CornerMaxTileEvaluator:ICacheableEvaluator
    {
        public double Evaluate(IGame game)
        {
            var corenerSquares = new[] {0, 3, 12, 15};
            var maxTile = game.Grid.Squares.Max(x => x.GetSafeTileValue());
            var cornerTiles = corenerSquares.Select(x => game.Grid.Squares.ElementAt(x).GetSafeTileValue());
            return cornerTiles.Contains(maxTile) ? maxTile : 0;
        }

        public string GetCacheKey(IGame game) => game.Grid.ToString();
    }
}