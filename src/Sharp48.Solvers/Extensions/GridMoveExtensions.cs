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
    }
}