using System;
using System.Threading.Tasks;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.Solvers
{
    public class RandomSolver : ISolver
    {
        private static readonly Move[] Moves = {Move.Up, Move.Right, Move.Down, Move.Left};
        private readonly Random _random = new Random();

        public Move GetBestMove(IGrid grid)
        {
            Task.Delay(TimeSpan.FromMilliseconds(300)).Wait();
            return Moves[_random.Next(Moves.Length)];
        }
    }
}