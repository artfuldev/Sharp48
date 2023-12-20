namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class MonotonicityEvaluator : RowEvaluatorBase
    {
        protected override double EvaluateImplementation(uint[] tiles)
        {
            var monotonicityLeft = 0d;
            var monotonicityRight = 0d;
            for (var i = 1; i < 4; ++i)
            {
                if (tiles[i - 1] > tiles[i])
                    monotonicityLeft += tiles[i - 1] - tiles[i];
                else
                    monotonicityRight += tiles[i] - tiles[i - 1];
            }
            return Math.Min(monotonicityLeft, monotonicityRight);
        }
    }
}