using System;
using System.Collections.Generic;
using System.Linq;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.Solvers.Extensions
{
    internal static class GridMoveExtensions
    {
        public static IEnumerable<Move> GetPossibleMoves(this IGrid grid)
        {
            return grid.Rows.SelectMany(x => x.GetPossibleMoves())
                .Concat(grid.Columns.SelectMany(x => x.GetPossibleMoves()))
                .Distinct();
        }

        public static IEnumerable<IGrid> GetPossibleGenerations(this IGrid grid)
        {
            var gridString = grid.ToString();
            var squares = gridString.Split(new[] {Environment.NewLine, ","}, StringSplitOptions.None);
            for (var i = 0; i < 16; i++)
                if (string.IsNullOrWhiteSpace(squares[i]))
                {
                    var squaresCopy2 = squares.Select(x => x).ToArray();
                    squaresCopy2[i] = "2";
                    yield return Grid.Parse(string.Join(",", squaresCopy2));
                    var squaresCopy4 = squares.Select(x => x).ToArray();
                    squaresCopy4[i] = "4";
                    yield return Grid.Parse(string.Join(",", squaresCopy4));
                }
        }

        public static IGrid MakeMove(this IGrid grid, Move move, out uint score)
        {
            var squares = new ISquare[4][];
            for (var i = 0; i < 4; i++)
                squares[i] = new ISquare[4];
            score = 0;
            switch (move)
            {
                case Move.Up:
                case Move.Down:
                    for (var i = 0; i < 4; i++)
                    {
                        uint localScore;
                        var localSquares = grid.Columns.ElementAt(i).MakeMove(move, out localScore).ToArray();
                        score += localScore;
                        for (var j = 0; j < 4; j++)
                            squares[j][i] = localSquares[j];
                    }
                    break;
                case Move.Right:
                case Move.Left:
                    for (var i = 0; i < 4; i++)
                    {
                        uint localScore;
                        var localSquares = grid.Rows.ElementAt(i).MakeMove(move, out localScore).ToArray();
                        score += localScore;
                        for (var j = 0; j < 4; j++)
                            squares[i][j] = localSquares[j];
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }
            var newSquares = new ISquare[16];
            for (var i = 0; i < 4; i++)
                for (var j = 0; j < 4; j++)
                    newSquares[i*4 + j] = squares[i][j];
            var gridString = string.Join(",", newSquares.ToList());
            return Grid.Parse(gridString);
        }
    }
}