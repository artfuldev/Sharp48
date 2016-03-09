namespace Sharp48.Core.PlayArea
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