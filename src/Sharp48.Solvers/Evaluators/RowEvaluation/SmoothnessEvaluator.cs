using System;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class SmoothnessEvaluator : RowEvaluatorBase
    {
        private readonly double _factor;

        public SmoothnessEvaluator(double factor)
        {
            _factor = factor;
        }

        protected override double EvaluateImplementation(byte[] tiles)
        {
            var score = 0d;
            for (var i = 0; i < 4; i++)
            {
                var tile = tiles[i];
                if (tile == 0)
                    continue;
                if (i < 3)
                    score -= Math.Pow(_factor, Math.Abs(tile - tiles[i + 1]));
                if (i > 0)
                    score -= Math.Pow(_factor, Math.Abs(tile - tiles[i - 1]));
            }
            return score;
        }
    }
}