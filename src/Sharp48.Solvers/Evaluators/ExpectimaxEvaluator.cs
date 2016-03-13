using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class ExpectimaxEvaluator : IEvaluator
    {
        private readonly IEvaluator _evaluator;
        private readonly byte _depth;
        private readonly IDictionary<string, double> _hashTable = new Dictionary<string, double>();

        private double EvaluateInternal(IGame game)
        {
            var key = game.Grid.ToString();
            if (!_hashTable.ContainsKey(key))
                _hashTable[key] = _evaluator.Evaluate(game);
            return _hashTable[key];
        }

        public ExpectimaxEvaluator(IEvaluator evaluator, byte depth)
        {
            _evaluator = evaluator;
            _depth = depth;
        }

        public double Evaluate(IGame game) => ExpectiMaxScore(game, _depth, true);

        private double ExpectiMaxScore(IGame game, byte depth, bool randomEvent)
        {
            if (game.Over || depth == 0)
                return EvaluateInternal(game);
            var key = game.Grid.ToString();
            if (_hashTable.ContainsKey(key))
                return _hashTable[key];
            double alpha;
            // Random event at node
            if (randomEvent)
            {
                var possibleGenerations = game.GetPossibleGenerations().ToList();
                var probability = (double) 1/possibleGenerations.Count;
                alpha = possibleGenerations.Sum(node => probability*ExpectiMaxScore(node, (byte) (depth - 1), false));
            }
            // If we are to play at node
            else
            {
                var possibleGames = game.GetPossibleMoves().Select(game.MakeMove);
                alpha = possibleGames.Max(x => ExpectiMaxScore(x, (byte) (depth - 1), true));
            }
            return alpha;
        }
    }
}