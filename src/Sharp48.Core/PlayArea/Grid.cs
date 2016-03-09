using System.Collections.Generic;
using System.Linq;
using Sharp48.Core.Tiles;

namespace Sharp48.Core.PlayArea
{
    public class Grid : IGrid
    {
        public Grid()
        {
            var columnSquares = new List<ISquare>[4];
            var columns = new IColumn[4];
            for (var i = 0; i < 4; i++)
            {
                columnSquares[i] = new ISquare[4].ToList();
                columns[i] = new Column(columnSquares[i]);
            }

            var rowSquares = new List<ISquare>[4];
            var rows = new IRow[4];
            for (var i = 0; i < 4; i++)
            {
                rowSquares[i] = new ISquare[4].ToList();
                rows[i] = new Row(rowSquares[i]);
            }

            var squares = new ISquare[16];
            for (var i = 0; i < 4; i++)
                for (var j = 0; j < 4; j++)
                {
                    var squareIndex = i*4 + j;
                    var square = new Square();
                    squares[squareIndex] = square;
                    rowSquares[i][j] = square;
                    columnSquares[j][i] = square;
                }

            Squares = squares.ToList().AsReadOnly();
            Rows = rows.ToList().AsReadOnly();
            Columns = columns.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<ISquare> Squares { get; }
        public IReadOnlyCollection<IRow> Rows { get; }
        public IReadOnlyCollection<IColumn> Columns { get; }

        public static IGrid ParseGrid(string gridString)
        {
            var grid = new Grid();
            var tiles = gridString.Split(',')
                .Select(x => string.IsNullOrWhiteSpace(x) ? (ITile) null : new Tile {Value = uint.Parse(x)})
                .ToArray();
            for (var i = 0; i < tiles.Length; i++)
            {
                if (tiles[i] != null)
                    grid.Squares.ElementAt(i).Tile = tiles[i];
            }
            return grid;
        }

        public override string ToString()
        {
            return string.Join("\n", Rows.Select(x => x.ToString()));
        }
    }
}