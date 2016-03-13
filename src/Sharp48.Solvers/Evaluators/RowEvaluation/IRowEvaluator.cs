using System.Collections.Generic;
using Sharp48.Core.PlayArea;

namespace Sharp48.Solvers.Evaluators.RowEvaluation
{
    public interface IRowEvaluator : ICacheableEvaluator
    {
        double Evaluate(IEnumerable<ISquare> squares);
    }
}