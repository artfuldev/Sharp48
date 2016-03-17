using System;
using System.Linq;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class MergeEvaluator : RowEvaluatorBase
    {

        protected override double EvaluateImplementation(byte[] tiles)
        {
            var merges = 0d;
            var prev = 0u;
            var counter = 0;
            foreach (var tile in tiles.Where(x => x != 0))
            {
                if (prev == tile)
                    counter++;
                else if (counter > 0)
                {
                    merges += Math.Pow(1 + counter, tile);
                    counter = 0;
                }
                prev = tile;
            }
            if (counter > 0)
            {
                merges += Math.Pow(1 + counter, tiles.Last());
            }
            return merges;
        }
    }
}