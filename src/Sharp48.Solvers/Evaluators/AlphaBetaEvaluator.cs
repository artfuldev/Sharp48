using System;
using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class AlphaBetaEvaluator : IEvaluator
    {
        private readonly IEvaluator _evaluator;
        private readonly byte _depth;

        public AlphaBetaEvaluator(IEvaluator evaluator, byte depth)
        {
            _evaluator = evaluator;
            _depth = depth;
        }

        public double Evaluate(IGame game) => AlphaBetaScore(game, _depth, double.NegativeInfinity, double.PositiveInfinity, false);

        private double AlphaBetaScore(IGame game, byte depth, double alpha, double beta, bool maximizingPlayer)
        {
            if (game.Over || depth == 0)
                return _evaluator.Evaluate(game);
            double score;
            // If we are to play at node
            if(maximizingPlayer)
            {
                score = double.NegativeInfinity;
                var possibleGames = game.GetPossibleMoves().Select(game.MakeMove);
                foreach (var nextGame in possibleGames)
                {
                    score = Math.Max(score, AlphaBetaScore(nextGame, (byte) (depth - 1), alpha, beta, false));
                    alpha = Math.Max(alpha, score);
                    // Beta Cut-Off
                    if (beta <= alpha)
                        break;
                }
                return score;
            }
            // If enemy is to play at node
            score = double.PositiveInfinity;
            var possibleGenerations = game.GetPossibleGenerations().ToList();
            foreach (var nextGame in possibleGenerations)
            {
                score = Math.Min(score, AlphaBetaScore(nextGame, (byte) (depth - 1), alpha, beta, true));
                beta = Math.Min(beta, score);
                // Alpha Cut-Off
                if (beta <= alpha)
                    break;
            }
            return score;
        }
    }
}