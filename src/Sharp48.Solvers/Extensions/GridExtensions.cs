using System.Collections.Generic;
using Sharp48.Core;
using Sharp48.Core.Moves;

namespace Sharp48.Solvers.Extensions
{
    public static class GridExtensions
    {
        public static ushort[] GetColumns(this ulong grid)
        {
            return new ushort[0];
        }

        public static ushort[] GetRows(this ulong grid)
        {
            return new ushort[0];
        }

        public static byte[] AsBytes(this ushort row)
        {
            return new byte[0];
        }

        public static ushort Reverse(this ushort row)
        {
            return (ushort) ((row >> 12) | ((row >> 4) & 0x00F0) | ((row << 4) & 0x0F00) | (row << 12));
        }

        public static bool NoMovesLeft(this ulong grid)
        {
            return false;
        }

        public static byte EmptySquaresCount(this ulong grid)
        {
            return 0;
        }

        public static IEnumerable<ulong> GetPossible2Generations(this ulong grid)
        {
            return new ulong[0];
        }

        public static IEnumerable<ulong> GetPossible4Generations(this ulong grid)
        {
            return new ulong[0];
        }

        public static IEnumerable<Move> GetPossibleMoves(this ulong grid)
        {
            return new Move[0];
        }

        public static ulong MakeMove(this ulong grid, Move move)
        {
            return 0ul;
        }

        public static ulong AsGrid(this IGame game)
        {
            return 0ul;
        }
    }
}