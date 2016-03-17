using System;
using System.Linq;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class TileSumEvaluator : RowEvaluatorBase
    {
        private readonly double _factor;

        public TileSumEvaluator(double factor)
        {
            _factor = factor;
        }

        protected override double EvaluateImplementation(byte[] tiles) => tiles.Sum(tile => Math.Pow(_factor, tile));
    }
}