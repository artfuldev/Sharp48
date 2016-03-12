using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public class ScoreEvaluator : ICacheableEvaluator
    {
        public double Evaluate(IGame game) => game.Score;
        public string GetCacheKey(IGame game) => game.Grid.ToString();
    }
}