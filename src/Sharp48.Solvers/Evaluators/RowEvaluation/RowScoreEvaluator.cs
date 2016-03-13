using System;
using System.Linq;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class RowScoreEvaluator : RowEvaluatorBase
    {
        private readonly double _factor;
        // 3.5
        public RowScoreEvaluator(double factor)
        {
            _factor = factor;
        }

        protected override double EvaluateImplementation(uint[] tiles) => tiles.Sum(tile => Math.Pow(tile, _factor));
    }
}