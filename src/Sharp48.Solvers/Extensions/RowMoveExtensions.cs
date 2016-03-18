using System;
using System.Collections.Generic;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;
using System.Linq;

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

        public static IRow MakeMove(this IRow row, Move move, out uint score)
        {
            switch (move)
            {
                case Move.Up:
                case Move.Down:
                    score = 0;
                    return new Row(new List<ISquare>());
                case Move.Right:
                    return new Row(row.MoveRight(out score).ToList());
                case Move.Left:
                    return new Row(row.Reverse().MoveRight(out score).Reverse().ToList());
                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }
        }
    }
}