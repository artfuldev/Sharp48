using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class ExpectimaxEvaluator : IEvaluator
    {
        private readonly IEvaluator _evaluator = new ScoreEvaluator();
        private readonly IDictionary<string, double> _hashTable = new Dictionary<string, double>();

        public double Evaluate(IGame game) => ExpectiMaxScore(game, CalculateDepth(game));

        private double Score(IGame game)
        {
            var key = game.Grid.ToString();
            if (!_hashTable.ContainsKey(key))
                _hashTable[key] = _evaluator.Evaluate(game);
            return _hashTable[key];
        }

        private double ExpectiMaxScore(IGame game, byte depth)
        {
            if (game.Over || depth == 0)
                return Score(game);
            double alpha;
            // Random event at node
            if (depth%2 == 0)
            {
                var possibleGenerations = game.GetPossibleGenerations().ToList();
                var probability = (double) 1/possibleGenerations.Count;
                alpha = possibleGenerations.Sum(node => probability*ExpectiMaxScore(node, (byte) (depth - 1)));
            }
            // If we are to play at node
            else
            {
                var possibleGames = game.GetPossibleMoves().Select(game.MakeMove);
                alpha = possibleGames.Max(x => ExpectiMaxScore(x, (byte) (depth - 1)));
            }
            return alpha;
        }

        private static byte CalculateDepth(IGame game) => 4;
    }
}