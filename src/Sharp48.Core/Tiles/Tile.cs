namespace Sharp48.Core.Tiles
{
    public class Tile : ITile
    {
        public uint Value { get; set; } = 2;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}