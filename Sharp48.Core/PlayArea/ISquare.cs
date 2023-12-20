using Sharp48.Core.Tiles;

namespace Sharp48.Core.PlayArea
{
    /// <summary>
    ///     Represents a square in the 2048 game grid.
    /// </summary>
    public interface ISquare
    {
        /// <summary>
        ///     The tile, if any, present in the square.
        /// </summary>
        ITile Tile { get; set; }
    }
}