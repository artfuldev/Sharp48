using System;
using System.Collections.Generic;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;
using System.Linq;

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

        public static IColumn MakeMove(this IColumn column, Move move, out uint score)
        {
            switch (move)
            {
                case Move.Right:
                case Move.Left:
                    score = 0;
                    return new Column(new List<ISquare>());
                case Move.Down:
                    return new Column(column.MoveRight(out score).ToList());
                case Move.Up:
                    return new Column(column.Reverse().MoveRight(out score).Reverse().ToList());
                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }
        }
    }
}