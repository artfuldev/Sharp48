using System;
using System.Linq;
using Sharp48.Core;
using Sharp48.Core.Moves;
using Sharp48.Solvers.Evaluators;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers
{
    public class IntelligentSolver : ISolver
    {
        private readonly IEvaluator _evaluator;

        public IntelligentSolver(IEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public Move GetBestMove(IGame game)
        {
            var possibleMoves = game.GetPossibleMoves();
            var movesToScoreDictionary = possibleMoves.ToDictionary(x => x,
                x => _evaluator.Evaluate(game.MakeMove(x)));
            var bestScore = movesToScoreDictionary.Max(x => x.Value);
            return movesToScoreDictionary.First(x => Math.Abs(x.Value - bestScore) < 0.1).Key;
        }
    }
}