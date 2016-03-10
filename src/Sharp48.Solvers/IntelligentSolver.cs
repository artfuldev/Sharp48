using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Core.Moves;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers
{
    public class IntelligentSolver : ISolver
    {
        private readonly byte _depth;
        private readonly IDictionary<string, double> _hashTable = new Dictionary<string, double>();

        public IntelligentSolver(byte depth)
        {
            _depth = depth;
        }

        public Move GetBestMove(IGame game)
        {
            var possibleMoves = game.GetPossibleMoves();
            var movesToScoreDictionary = possibleMoves.ToDictionary(x => x, x => ExpectiMaxScore(game.MakeMove(x), (byte)(_depth * 2)));
            var bestScore = movesToScoreDictionary.Max(x => x.Value);
            return movesToScoreDictionary.First(x => Math.Abs(x.Value - bestScore) < 0.1).Key;
        }

        public double Score(IGame game)
        {
            var key = game.Grid.ToString();
            if (!_hashTable.ContainsKey(key))
                _hashTable[key] = game.Score;
            return _hashTable[key];
        }

        public double ExpectiMaxScore(IGame game, byte depth)
        {
            if (game.Over || depth == 0)
                return Score(game);
            var key = game.Grid.ToString();
            if (_hashTable.ContainsKey(key))
                return _hashTable[key];
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