using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public interface IEvaluator
    {
        double Evaluate(IGame game);
    }
}