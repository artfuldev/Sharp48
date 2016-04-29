using Sharp48.Core.PlayArea;

namespace Sharp48.Core
{
    /// <summary>
    /// Represents a game of 2048.
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        /// Instantiates a <seealso cref="Game"/> with a grid and a game over flag.
        /// </summary>
        /// <param name="grid">The grid of the game.</param>
        /// <param name="over">The game over status of the game.</param>
        public Game(IGrid grid, bool over)
        {
            Grid = grid;
            Over = over;
        }

        /// <summary>
        ///     The grid of the game.
        /// </summary>
        public IGrid Grid { get; }

        /// <summary>
        ///     Describes whether the game is over.
        /// </summary>
        public bool Over { get; }
    }
}