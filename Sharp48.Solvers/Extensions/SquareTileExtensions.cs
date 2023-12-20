using Sharp48.Core.PlayArea;

namespace Sharp48.Solvers.Extensions
{
    internal static class SquareTileExtensions
    {
        public static bool HasTile(this ISquare square)
        {
            return square.GetSafeTileValue() != 0;
        }

        public static uint GetSafeTileValue(this ISquare square)
        {
            return square?.Tile?.Value ?? 0;
        }
    }
}