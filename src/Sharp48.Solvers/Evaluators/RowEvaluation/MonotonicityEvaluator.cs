using System;
using System.Linq;
using Sharp48.Solvers.Extensions;

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
            var logs = tiles.Select(x => x.GetLog2Value()).ToArray();
            var monotonicityLeft = 0d;
            var monotonicityRight = 0d;
            for (var i = 1; i < 4; ++i)
            {
                if (logs[i - 1] > logs[i])
                    monotonicityLeft += Math.Pow(logs[i - 1], _factor) - Math.Pow(logs[i], _factor);
                else
                    monotonicityRight += Math.Pow(logs[i], _factor) - Math.Pow(logs[i - 1], _factor);
            }
            return Math.Min(monotonicityLeft, monotonicityRight);
        }
    }
}