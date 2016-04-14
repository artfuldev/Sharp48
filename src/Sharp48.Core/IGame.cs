using Sharp48.Core.PlayArea;

namespace Sharp48.Core
{
    /// <summary>
    ///     Represents a game of 2048
    /// </summary>
    public interface IGame
    {
        /// <summary>
        ///     The grid of the game.
        /// </summary>
        IGrid Grid { get; }

        /// <summary>
        ///     Describes whether the game is over.
        /// </summary>
        bool Over { get; }

        /// <summary>
        ///     The last generated tile's value.
        /// </summary>
        byte LastGeneratedTileValue { get; }
    }
}