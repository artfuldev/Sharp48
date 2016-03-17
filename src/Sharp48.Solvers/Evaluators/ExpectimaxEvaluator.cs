using System;
using System.Linq;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class ExpectimaxEvaluator : IEvaluator
    {
        private readonly IEvaluator _evaluator;
        private readonly double _threshold = 0.001;

        public ExpectimaxEvaluator(IEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public double Evaluate(ulong grid)
            => ExpectiMaxScore(grid, (byte) Math.Max(grid.EmptySquaresCount() - 2, 3), true, 1);

        private double ExpectiMaxScore(ulong grid, byte depth, bool randomEvent, double cumulativeProbability)
        {
            if (grid.NoMovesLeft())
                return double.NegativeInfinity;
            if (depth == 0 || cumulativeProbability < _threshold)
                return _evaluator.Evaluate(grid);
            double alpha;
            // Random event at node
            if (randomEvent)
            {
                var emptySquaresCount = grid.EmptySquaresCount();
                cumulativeProbability /= emptySquaresCount;
                var gamesWith2 = grid.GetPossible2Generations().ToList();
                alpha =
                    gamesWith2.Sum(
                        node => 0.9*ExpectiMaxScore(node, (byte) (depth - 1), false, cumulativeProbability*0.9));
                var gamesWith4 = grid.GetPossible4Generations().ToList();
                alpha +=
                    gamesWith4.Sum(
                        node => 0.1*ExpectiMaxScore(node, (byte) (depth - 1), false, cumulativeProbability*0.1));
            }
            // If we are to play at node
            else
            {
                var possibleGames = grid.GetPossibleMoves().Select(move => grid.MakeMove(move));
                alpha = possibleGames.Max(x => ExpectiMaxScore(x, (byte) (depth - 1), true, cumulativeProbability));
            }
            return alpha;
        }
    }
}