using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public class ScoreEvaluator : IEvaluator
    {
        public double Evaluate(IGame game)
        {
            return game.Score;
        }
    }
}