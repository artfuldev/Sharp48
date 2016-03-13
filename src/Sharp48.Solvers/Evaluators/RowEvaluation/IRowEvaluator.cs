namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public interface IRowEvaluator : IEvaluator
    {
        void Preload();
        double Evaluate(byte[] tiles);
    }
}