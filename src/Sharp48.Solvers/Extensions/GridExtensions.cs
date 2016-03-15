using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sharp48.Core;
using Sharp48.Core.Moves;
using Sharp48.Solvers.MoveExecutors;

namespace Sharp48.Solvers.Extensions
{
    public static class GridExtensions
    {
        private const ulong RowMask = 0xFFFFUL;
        private const ulong ColumnMask = 0x000F000F000F000FUL;
        private static readonly IMoveExecutor MoveExecutor = new MoveExecutor();

        public static ushort[] GetColumns(this ulong grid)
        {
            return grid.Transpose().GetRows();
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

        // Transpose rows/columns in a board:
        //   0123       048c
        //   4567  -->  159d
        //   89ab       26ae
        //   cdef       37bf
        public static ulong Transpose(this ulong grid)
        {
            var a1 = grid & 0xF0F00F0FF0F00F0FUL;
            var a2 = grid & 0x0000F0F00000F0F0UL;
            var a3 = grid & 0x0F0F00000F0F0000UL;
            var a = a1 | (a2 << 12) | (a3 >> 12);
            var b1 = a & 0xFF00FF0000FF00FFUL;
            var b2 = a & 0x00FF00FF00000000UL;
            var b3 = a & 0x00000000FF00FF00UL;
            return b1 | (b2 >> 24) | (b3 << 24);
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

        public static ushort ToRow(this byte[] tiles)
        {
            return tiles.Aggregate((ushort) 0, (current, row) => (ushort) (current << 4 | row));
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
            var copy = grid;
            var tile2 = 1ul;
            while (tile2 != 0)
            {
                if ((copy & 0xf) == 0)
                    yield return grid | tile2;
                copy >>= 4;
                tile2 <<= 4;
            }
        }

        public static IEnumerable<ulong> GetPossible4Generations(this ulong grid)
        {
            var copy = grid;
            var tile4 = 2ul;
            while (tile4 != 0)
            {
                if ((copy & 0xf) == 0)
                    yield return grid | tile4;
                copy >>= 4;
                tile4 <<= 4;
            }
        }

        public static IEnumerable<Move> GetPossibleMoves(this ulong grid)
        {
            return MoveExecutor.GetPossibleMoves(grid);
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

        public static ulong ToGrid(this ushort[] rows)
        {
            return rows.Aggregate(0ul, (current, row) => current << 16 | row);
        }
    }
}