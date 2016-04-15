using Sharp48.Core.PlayArea;

namespace Sharp48.Core
{
    public class Game : IGame
    {
        public Game(IGrid grid, bool over)
        {
            Grid = grid;
            Over = over;
        }

        public IGrid Grid { get; }
        public bool Over { get; }
    }
}