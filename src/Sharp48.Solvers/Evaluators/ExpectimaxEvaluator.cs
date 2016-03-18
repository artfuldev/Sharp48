﻿using Sharp48.Core;

namespace Sharp48.Solvers.Evaluators
{
    public class ExpectimaxEvaluator : IEvaluator
    {
        private readonly IEvaluator _evaluator;
        private readonly double _threshold = 0.0001;
        private readonly int _maxEntries = 2048;

        public ExpectimaxEvaluator(IEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public double Evaluate(IGame game)
            => ExpectiMaxScore(game, 3, true, 1);

        private double ExpectiMaxScore(IGame game, byte depth, bool randomEvent, double cumulativeProbability)
        {
            if (game.Over)
                return double.NegativeInfinity;
            if (depth == 0 || cumulativeProbability < _threshold)
                return _evaluator.Evaluate(game);
            double alpha;
            // Random event at node
            if (randomEvent)
            {
                var emptySquaresCount = game.EmptySquaresCount();
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
                var possibleGames = game.GetPossibleMoves().Select(move => game.MakeMove(move));
                alpha = possibleGames.Max(x => ExpectiMaxScore(x, (byte) (depth - 1), true, cumulativeProbability));
            }
            return alpha;
        }
    }
}