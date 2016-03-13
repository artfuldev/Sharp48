using System.Collections.Generic;
using System.Linq;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class ExpectimaxEvaluator : IEvaluator
    {
        private readonly byte _depth;
        private readonly IEvaluator _evaluator;
        private readonly IDictionary<ulong, double> _hashTable = new Dictionary<ulong, double>();
        private readonly double _threshold;

        public ExpectimaxEvaluator(IEvaluator evaluator, byte depth, double threshold)
        {
            _evaluator = evaluator;
            _depth = depth;
            _threshold = threshold;
        }

        public double Evaluate(ulong grid) => ExpectiMaxScore(grid, _depth, true, 1);

        private double EvaluateInternal(ulong grid)
        {
            if (!_hashTable.ContainsKey(grid))
                _hashTable[grid] = _evaluator.Evaluate(grid);
            return _hashTable[grid];
        }

        private double ExpectiMaxScore(ulong grid, byte depth, bool randomEvent, double cumulativeProbability)
        {
            if (grid.NoMovesLeft() || depth == 0 || cumulativeProbability < _threshold)
                return EvaluateInternal(grid);
            if (_hashTable.ContainsKey(grid))
                return _hashTable[grid];
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