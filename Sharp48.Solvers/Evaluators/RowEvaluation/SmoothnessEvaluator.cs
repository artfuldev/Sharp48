namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class SmoothnessEvaluator : RowEvaluatorBase
    {
        protected override double EvaluateImplementation(uint[] tiles)
        {
            var score = 0d;
            for (var i = 0; i < 4; i++)
            {
                var tile = tiles[i];
                if (tile == 0)
                    continue;
                if (i < 3)
                    score -= Math.Abs(tile - tiles[i + 1]);
                if (i > 0)
                    score -= Math.Abs(tile - tiles[i - 1]);
            }
            return score;
        }
    }
}