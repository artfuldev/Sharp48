using System;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class MonotonicityEvaluator : RowEvaluatorBase
    {
        private readonly double _factor;

        public MonotonicityEvaluator(double factor)
        {
            _factor = factor;
        }

        protected override double EvaluateImplementation(uint[] tiles)
        {
            var monotonicityLeft = 0d;
            var monotonicityRight = 0d;
            for (var i = 1; i < 4; ++i)
            {
                if (tiles[i - 1] > tiles[i])
                    monotonicityLeft += Math.Pow(tiles[i - 1], _factor) - Math.Pow(tiles[i], _factor);
                else
                    monotonicityRight += Math.Pow(tiles[i], _factor) - Math.Pow(tiles[i - 1], _factor);
            }
            return Math.Min(monotonicityLeft, monotonicityRight);
        }
    }
}