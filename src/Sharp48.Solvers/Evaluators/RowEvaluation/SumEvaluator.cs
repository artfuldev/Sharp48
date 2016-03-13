using System;
using System.Linq;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class SumEvaluator : RowEvaluatorBase
    {
        private readonly double _factor;

        public SumEvaluator(double factor)
        {
            _factor = factor;
        }

        protected override double EvaluateImplementation(byte[] tiles) => tiles.Sum(tile => Math.Pow(tile, _factor));
    }
}