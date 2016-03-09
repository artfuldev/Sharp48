using Sharp48.Core.Tiles;

namespace Sharp48.Core.PlayArea
{
    public class Square : ISquare
    {
        public ITile Tile { get; set; } = null;

        public override string ToString()
        {
            return Tile?.ToString() ?? " ";
        }
    }
}