using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Core.Moves;
using Sharp48.Solvers.Evaluators;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers
{
    public class IntelligentSolver : ISolver
    {
        private readonly IDictionary<string, double> _hashTable = new Dictionary<string, double>();
        private readonly IEvaluator _evaluator = new ScoreEvaluator();

        public Move GetBestMove(IGame game)
        {
            var n = 2*Math.Log10(game.Grid.Squares.Max(x => x.GetSafeTileValue()))/Math.Log10(2);
            var depth = Convert.ToByte(n > 4 ? 4 : n);
            var possibleMoves = game.GetPossibleMoves();
            var movesToScoreDictionary = possibleMoves.ToDictionary(x => x,
                x => ExpectiMaxScore(game.MakeMove(x), depth));
            var bestScore = movesToScoreDictionary.Max(x => x.Value);
            return movesToScoreDictionary.First(x => Math.Abs(x.Value - bestScore) < 0.1).Key;
        }

        public double Score(IGame game)
        {
            var key = game.Grid.ToString();
            if (!_hashTable.ContainsKey(key))
                _hashTable[key] = _evaluator.Evaluate(game);
            return _hashTable[key];
        }

        public double ExpectiMaxScore(IGame game, byte depth)
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
    }
}