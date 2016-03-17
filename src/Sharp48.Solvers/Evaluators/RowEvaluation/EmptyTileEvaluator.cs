using System;
using System.Linq;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class EmptyTileEvaluator : RowEvaluatorBase
    {

        protected override double EvaluateImplementation(byte[] tiles)
            => Math.Pow(tiles.Count(x => x == 0), Math.Pow(2, tiles.Max()));
    }
}