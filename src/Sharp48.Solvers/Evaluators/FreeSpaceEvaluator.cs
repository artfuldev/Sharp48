using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class FreeSpaceEvaluator : ICacheableEvaluator
    {
        public double Evaluate(IGame game) => game.Grid.Squares.Count(x => x.GetSafeTileValue() == 0)*32768D;
        public string GetCacheKey(IGame game) => game.Grid.ToString();
    }
}