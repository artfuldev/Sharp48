namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class TileSumEvaluator : RowEvaluatorBase
    {
        protected override double EvaluateImplementation(uint[] tiles) => tiles.Sum(x => (double) x);
    }
}