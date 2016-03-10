using System;
using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
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
            for(var i=0;i<16;i++)
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
    }
}