using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class MergesAwayEvaluator : IEvaluator
    {
        private readonly IDictionary<uint, double> _mergesAway = new Dictionary<uint, double>();

        public double Evaluate(IGame game)
            =>
                game.Grid.Squares.Select(x => x.GetSafeTileValue())
                    .Where(x => x != 0)
                    .Sum(x => -MergesAway(x));

        private double MergesAway(uint tile)
        {
            if (_mergesAway.ContainsKey(tile))
                return _mergesAway[tile];
            return _mergesAway[tile] = tile == 2 ? 1 : 2*MergesAway(tile/2);
        }
    }
}