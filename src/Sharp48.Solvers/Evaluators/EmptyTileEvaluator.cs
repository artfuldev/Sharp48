using System;
using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class EmptyTileEvaluator : IEvaluator
    {
        public double Evaluate(IGame game)
            => game.Grid.Squares.Count(x => x.GetSafeTileValue() == 0)*
               game.Grid.Squares.Max(x => x.GetSafeTileValue());
    }
}