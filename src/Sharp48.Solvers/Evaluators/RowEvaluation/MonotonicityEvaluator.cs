using System;
using System.Linq;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class MonotonicityEvaluator : RowEvaluatorBase
    {
        private readonly double _factor;

        public MonotonicityEvaluator(double factor)
        {
            _factor = factor;
        }

        protected override double EvaluateImplementation(byte[] tiles)
        {
            var monotonicityLeft = true;
            var monotonicityRight = true;
            var largest = tiles.Max();
            for (var i = 1; i < 4; ++i)
            {
                if (tiles[i - 1] <= tiles[i] && tiles[i - 1] != 0) continue;
                monotonicityRight = false;
                break;
            }
            for (var i = 1; i < 4; ++i)
            {
                if (tiles[i - 1] >= tiles[i] && tiles[i] != 0) continue;
                monotonicityLeft = false;
                break;
            }
            return monotonicityLeft || monotonicityRight ? Math.Pow(largest, _factor) : 0;
        }
    }
}