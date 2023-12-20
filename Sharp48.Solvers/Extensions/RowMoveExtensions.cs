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

        public static IRow MakeMove(this IRow row, Move move)
        {
            switch (move)
            {
                case Move.Up:
                case Move.Down:
                    return new Row(new List<ISquare>());
                case Move.Right:
                    return new Row(row.MoveRight().ToList());
                case Move.Left:
                    return new Row(row.Reverse().MoveRight().Reverse().ToList());
                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }
        }
    }
}