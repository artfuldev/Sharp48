using Sharp48.Core.Tiles;

namespace Sharp48.Core.PlayArea
{
    /// <summary>
    ///     Represents a square in the game grid of 2048.
    /// </summary>
    public class Square : ISquare
    {
        /// <summary>
        ///     The tile, if any, present in the square.
        /// </summary>
        public ITile Tile { get; set; } = null;

        /// <summary>
        ///     The string representation of a square. Allows for easier debugging.
        /// </summary>
        /// <returns>The string representation of a square, with its tile.</returns>
        public override string ToString()
        {
            return Tile?.ToString() ?? " ";
        }
    }
}