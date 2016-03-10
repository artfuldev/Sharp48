using System.Collections.Generic;
using System.Linq;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.Solvers.Extensions
{
    internal static class ColumnMoveExtensions
    {
        public static IEnumerable<Move> GetPossibleMoves(this IColumn column)
        {
            if (column.IsRightMovePossible())
                yield return Move.Down;
            if (column.Reverse().IsRightMovePossible())
                yield return Move.Up;
        }
    }
}