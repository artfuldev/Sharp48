using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class ExpectimaxEvaluator : IEvaluator
    {
        private readonly byte _depth;
        private readonly IEvaluator _evaluator;
        private readonly IDictionary<string, double> _hashTable = new Dictionary<string, double>();
        private readonly double _threshold;

        public ExpectimaxEvaluator(IEvaluator evaluator, byte depth, double threshold)
        {
            _evaluator = evaluator;
            _depth = depth;
            _threshold = threshold;
        }

        private double EvaluateInternal(IGame game)
        {
            var key = game.Grid.ToString();
            if (!_hashTable.ContainsKey(key))
                _hashTable[key] = _evaluator.Evaluate(game);
            return _hashTable[key];
        }

        public double Evaluate(IGame game) => ExpectiMaxScore(game, _depth, true, 1);

        private double ExpectiMaxScore(IGame game, byte depth, bool randomEvent, double cumulativeProbability)
        {
            if (game.Over || depth == 0 || cumulativeProbability < _threshold)
                return EvaluateInternal(game);
            var key = game.Grid.ToString();
            if (_hashTable.ContainsKey(key))
                return _hashTable[key];
            double alpha;
            // Random event at node
            if (randomEvent)
            {
                var emptySquaresCount = game.Grid.Squares.Count(x => x.GetSafeTileValue() == 0);
                cumulativeProbability /= emptySquaresCount;
                var gamesWith2 = game.GetPossible2Generations().ToList();
                alpha =
                    gamesWith2.Sum(
                        node => 0.9*ExpectiMaxScore(node, (byte) (depth - 1), false, cumulativeProbability*0.9));
                var gamesWith4 = game.GetPossible4Generations().ToList();
                alpha +=
                    gamesWith4.Sum(
                        node => 0.1*ExpectiMaxScore(node, (byte) (depth - 1), false, cumulativeProbability*0.1));
            }
            // If we are to play at node
            else
            {
                var possibleGames = game.GetPossibleMoves().Select(game.MakeMove);
                alpha = possibleGames.Max(x => ExpectiMaxScore(x, (byte) (depth - 1), true, cumulativeProbability));
            }
            return alpha;
        }
    }
}