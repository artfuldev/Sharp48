using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public interface ICacheableEvaluator : IEvaluator
    {
        string GetCacheKey(IGame game);
    }
}