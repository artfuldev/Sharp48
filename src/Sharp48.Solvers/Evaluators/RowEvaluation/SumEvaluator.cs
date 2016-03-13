using System;
using System.Linq;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class SumEvaluator : RowEvaluatorBase
    {
        private readonly double _factor;

        public SumEvaluator(double factor)
        {
            _factor = factor;
        }

        protected override double EvaluateImplementation(uint[] tiles)
            => tiles.Sum(tile => Math.Pow(tile.GetLog2Value(), _factor));
    }
}