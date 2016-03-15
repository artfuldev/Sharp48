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
            var grid = game.AsGrid();
            var possibleMoves = grid.GetPossibleMoves().ToArray();
            var movesToScoreDictionary = possibleMoves.ToDictionary(x => x, x => _evaluator.Evaluate(grid.MakeMove(x)));
            var bestScore = movesToScoreDictionary.Max(x => x.Value);
            return double.IsNegativeInfinity(bestScore)
                ? possibleMoves.First()
                : movesToScoreDictionary.FirstOrDefault(
                    x => double.IsPositiveInfinity(x.Value) || Math.Abs(x.Value - bestScore) < 0.1).Key;
        }
    }
}