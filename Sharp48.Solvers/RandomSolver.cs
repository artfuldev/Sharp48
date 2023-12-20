using Sharp48.Core;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.Solvers
{
    public class RandomSolver : ISolver
    {
        private static readonly Move[] Moves = {Move.Up, Move.Right, Move.Down, Move.Left};
        private static readonly Random Random = new Random();

        public Move GetBestMove(IGame game) => GetBestMove(game.Grid);

        private static Move GetBestMove(IGrid grid)
        {
            Task.Delay(TimeSpan.FromMilliseconds(300)).Wait();
            return Moves[Random.Next(Moves.Length)];
        }
    }
}