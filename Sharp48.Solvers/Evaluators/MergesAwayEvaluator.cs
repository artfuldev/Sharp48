using System.Collections.Concurrent;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class MergesAwayEvaluator : IEvaluator
    {
        private readonly IDictionary<uint, double> _mergesAway = new ConcurrentDictionary<uint, double>();

        public double Evaluate(IGame game)
            =>
                game.Grid.Squares.Select(x => x.GetSafeTileValue())
                    .Where(x => x > 2)
                    .Sum(x => -MergesAway(x));

        private double MergesAway(uint tile)
        {
            return _mergesAway.ContainsKey(tile)
                ? _mergesAway[tile]
                : (_mergesAway[tile] = tile == 4 ? 1 : 2*MergesAway(tile/2));
        }
    }
}