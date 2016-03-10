using Sharp48.Core.PlayArea;

namespace Sharp48.Core
{
    public class Game : IGame
    {
        public Game(IGrid grid, bool over, long score)
        {
            Grid = grid;
            Over = over;
            Score = score;
        }

        public IGrid Grid { get; }
        public bool Over { get; }
        public long Score { get; }
    }
}