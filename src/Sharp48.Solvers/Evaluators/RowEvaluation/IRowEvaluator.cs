namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public interface IRowEvaluator : ICacheableEvaluator
    {
        void Preload();
        double Evaluate(uint[] tiles);
    }
}