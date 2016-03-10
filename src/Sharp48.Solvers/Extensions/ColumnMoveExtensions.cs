using System;
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

        public static IEnumerable<ISquare> MakeMove(this IColumn column, Move move, out uint score)
        {
            switch (move)
            {
                case Move.Right:
                case Move.Left:
                    score = 0;
                    return Enumerable.Empty<ISquare>();
                case Move.Down:
                    return column.MoveRight(out score);
                case Move.Up:
                    return column.Reverse().MoveRight(out score);
                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }
        }
    }
}