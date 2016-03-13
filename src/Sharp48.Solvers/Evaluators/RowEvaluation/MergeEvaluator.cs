using System.Linq;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class MergeEvaluator : RowEvaluatorBase
    {
        protected override double EvaluateImplementation(uint[] tiles)
        {
            var merges = 0;
            var prev = 0u;
            var counter = 0;
            foreach (var tile in tiles.Where(x => x != 0).Select(x=>x.GetLog2Value()))
            {
                if (prev == tile)
                    counter++;
                else if (counter > 0)
                {
                    merges += 1 + counter;
                    counter = 0;
                }
                prev = tile;
            }
            if (counter > 0)
            {
                merges += 1 + counter;
            }
            return merges;
        }
    }
}