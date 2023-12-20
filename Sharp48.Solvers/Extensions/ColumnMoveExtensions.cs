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

        public static IColumn MakeMove(this IColumn column, Move move)
        {
            switch (move)
            {
                case Move.Right:
                case Move.Left:
                    return new Column(new List<ISquare>());
                case Move.Down:
                    return new Column(column.MoveRight().ToList());
                case Move.Up:
                    return new Column(column.Reverse().MoveRight().Reverse().ToList());
                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }
        }
    }
}