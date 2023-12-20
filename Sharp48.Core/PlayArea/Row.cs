using System.Collections.ObjectModel;
using Sharp48.Core.Tiles;

namespace Sharp48.Core.PlayArea
{
    public class Row : ReadOnlyCollection<ISquare>, IRow
    {
        public Row(IList<ISquare> list) : base(list)
        {
        }

        public static Row Parse(string input)
        {
            var squares = Enumerable.Range(1, 4).Select(x => (ISquare) new Square()).ToList();
            var tiles = input.Split(',')
                .Select(x => string.IsNullOrWhiteSpace(x) ? (ITile) null : new Tile {Value = uint.Parse(x)})
                .ToArray();
            for (var i = 0; i < tiles.Length; i++)
            {
                if (tiles[i] != null)
                    squares[i].Tile = tiles[i];
            }
            return new Row(squares);
        }

        public override string ToString()
        {
            return string.Join(",", this.Select(x => x.ToString()));
        }
    }
}