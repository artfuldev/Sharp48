using System;
using System.Linq;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class AlphaBetaEvaluator : IEvaluator
    {
        private readonly byte _depth;
        private readonly IEvaluator _evaluator;

        public AlphaBetaEvaluator(IEvaluator evaluator, byte depth)
        {
            _evaluator = evaluator;
            _depth = depth;
        }

        public double Evaluate(ulong grid) => AlphaBetaScore(grid, _depth, double.NegativeInfinity, double.PositiveInfinity, false);

        private double AlphaBetaScore(ulong grid, byte depth, double alpha, double beta, bool maximizingPlayer)
        {
            if (grid.NoMovesLeft())
                return double.NegativeInfinity;
            if (depth == 0)
                return _evaluator.Evaluate(grid);
            double score;
            if (maximizingPlayer)
            {
                score = double.NegativeInfinity;
                foreach (var result in grid.GetPossibleMoves().Select(x=>grid.MakeMove(x)))
                {
                    score = Math.Max(score, AlphaBetaScore(result, (byte) (depth - 1), alpha, beta, false));
                    alpha = Math.Max(alpha, score);
                    if (beta <= alpha)
                        break; // beta cutoff
                }
                return score;
            }
            score = double.PositiveInfinity;
            foreach (var result in grid.GetPossible2Generations().Concat(grid.GetPossible4Generations()))
            {
                score = Math.Min(score, AlphaBetaScore(result, (byte) (depth - 1), alpha, beta, true));
                beta = Math.Min(beta, score);
                if (beta <= alpha)
                    break; // alpha cutoff
            }
            return score;
        }
    }
}