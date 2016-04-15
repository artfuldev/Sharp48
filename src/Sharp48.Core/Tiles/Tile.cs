namespace Sharp48.Core.Tiles
{
    /// <summary>
    ///     Represents a tile of the 2048 game.
    /// </summary>
    public class Tile : ITile
    {
        /// <summary>
        ///     The value of the tile as an unsigned integer. This is the same as the number displayed on the tile.
        /// </summary>
        public uint Value { get; set; } = 2;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}