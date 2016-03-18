using System;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;
using System.Linq;

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

        public double Evaluate(IGame game) => AlphaBetaScore(game, _depth, double.NegativeInfinity, double.PositiveInfinity, false);

        private double AlphaBetaScore(IGame game, byte depth, double alpha, double beta, bool maximizingPlayer)
        {
            if (game.Over)
                return double.NegativeInfinity;
            if (depth == 0)
                return _evaluator.Evaluate(game);
            double score;
            if (maximizingPlayer)
            {
                score = double.NegativeInfinity;
                foreach (var result in game.GetPossibleMoves().Select(game.MakeMove))
                {
                    score = Math.Max(score, AlphaBetaScore(result, (byte) (depth - 1), alpha, beta, false));
                    alpha = Math.Max(alpha, score);
                    if (beta <= alpha)
                        break; // beta cutoff
                }
                return score;
            }
            score = double.PositiveInfinity;
            foreach (var result in game.GetPossible2Generations().Concat(game.GetPossible4Generations()))
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