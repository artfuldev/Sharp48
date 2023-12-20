using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class CornerMaxTileEvaluator : IEvaluator
    {
        public double Evaluate(IGame game)
        {
            var maxTile = game.Grid.Squares.Max(x => x.GetSafeTileValue());
            var first = game.Grid.Rows.First();
            var last = game.Grid.Rows.Last();
            return first.First().GetSafeTileValue() == maxTile || first.Last().GetSafeTileValue() == maxTile ||
                   last.First().GetSafeTileValue() == maxTile || last.Last().GetSafeTileValue() == maxTile
                ? maxTile
                : 0;
        }
    }
}