using System.Collections.Generic;
using System.Linq;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.Solvers.Extensions
{
    internal static class RowMoveExtensions
    {
        public static IEnumerable<Move> GetPossibleMoves(this IRow row)
        {
            if (row.IsRightMovePossible())
                yield return Move.Right;
            if (row.Reverse().IsRightMovePossible())
                yield return Move.Left;
        }
    }
}