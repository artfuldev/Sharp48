using System;
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

        public static IEnumerable<ISquare> MakeMove(this IRow row, Move move, out uint score)
        {
            switch (move)
            {
                case Move.Up:
                case Move.Down:
                    score = 0;
                    return Enumerable.Empty<ISquare>();
                case Move.Right:
                    return row.MoveRight(out score);
                case Move.Left:
                    return row.Reverse().MoveRight(out score);
                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }
        }
    }
}