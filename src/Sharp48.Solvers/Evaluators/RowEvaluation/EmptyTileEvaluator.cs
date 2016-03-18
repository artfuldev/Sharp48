using System.Linq;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public class EmptyTileEvaluator : RowEvaluatorBase
    {
        protected override double EvaluateImplementation(uint[] tiles) => tiles.Count(x => x == 0);
    }
}