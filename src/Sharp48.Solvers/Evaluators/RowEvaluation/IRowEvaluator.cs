namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public interface IRowEvaluator : IEvaluator
    {
        double Evaluate(byte[] tiles);
    }
}