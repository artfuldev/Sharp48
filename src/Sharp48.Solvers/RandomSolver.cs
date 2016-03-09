using System;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.Solvers
{
    public class RandomSolver : ISolver
    {
        private static readonly Move[] Moves = {Move.Up, Move.Right, Move.Down, Move.Left};
        private readonly Random _random = new Random();
        public Move? GetBestMove(IGrid grid) => Moves[_random.Next(Moves.Length)];
    }
}