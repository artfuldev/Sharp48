using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class Reaching2048IsAWinEvaluator : IEvaluator
    {
        public double Evaluate(IGame game)
            => game.Grid.Squares.Select(x => x.GetSafeTileValue()).Any(x => x > 1024) ? double.PositiveInfinity : 0;
    }
}