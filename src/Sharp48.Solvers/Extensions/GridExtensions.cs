using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sharp48.Core;
using Sharp48.Core.Moves;

namespace Sharp48.Solvers.Extensions
{
    public static class GridExtensions
    {
        private const ulong RowMask = 0xFFFFUL;
        private const ulong ColumnMask = 0x000F000F000F000FUL;

        public static ushort[] GetColumns(this ulong grid)
        {
            return new ushort[0];
        }

        public static ushort[] GetRows(this ulong grid)
        {
            return new[]
            {
                (ushort) ((grid >> 48) & RowMask),
                (ushort) ((grid >> 32) & RowMask),
                (ushort) ((grid >> 16) & RowMask),
                (ushort) ((grid >> 0) & RowMask)
            };
        }

        public static byte[] AsTiles(this ushort row)
        {
            return new[]
            {
                (byte) ((row >> 12) & 0xf),
                (byte) ((row >> 8) & 0xf),
                (byte) ((row >> 4) & 0xf),
                (byte) ((row >> 0) & 0xf)
            };
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
            grid |= (grid >> 2) & 0x3333333333333333UL;
            grid |= grid >> 1;
            grid = ~grid & 0x1111111111111111UL;
            // At this point each nibble is:
            //  0 if the original nibble was non-zero
            //  1 if the original nibble was zero
            // Next sum them all
            grid += grid >> 32;
            grid += grid >> 16;
            grid += grid >> 8;
            grid += grid >> 4; // this can overflow to the next nibble if there were 16 empty positions
            return (byte) (grid & 0xf);
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
            var strings = game.Grid.Squares.Select(x =>
            {
                var value = x.Tile?.Value ?? 0;
                return value == 0 ? "0" : (Math.Log(value)/Math.Log(2)).ToString("N0", CultureInfo.InvariantCulture);
            });
            var aggregate = strings.Aggregate((current, next) => current + next);
            return ulong.Parse(aggregate, NumberStyles.AllowHexSpecifier);
        }
    }
}