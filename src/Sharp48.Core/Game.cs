using Sharp48.Core.PlayArea;

namespace Sharp48.Core
{
    public class Game : IGame
    {
        public Game(IGrid grid, bool gameOver, double score)
        {
            Grid = grid;
            GameOver = gameOver;
            Score = score;
        }

        public IGrid Grid { get; }
        public bool GameOver { get; }
        public double Score { get; }
    }
}