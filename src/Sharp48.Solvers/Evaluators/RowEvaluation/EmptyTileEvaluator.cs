using System;
using System.Linq;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class EmptyTileEvaluator : RowEvaluatorBase
    {

        protected override double EvaluateImplementation(byte[] tiles)
            => Math.Pow(tiles.Max(), tiles.Count(x => x == 0));
    }
}