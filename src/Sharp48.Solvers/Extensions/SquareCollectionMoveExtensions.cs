using System.Collections.Generic;
using System.Linq;
using Sharp48.Core.PlayArea;

namespace Sharp48.Solvers.Extensions
{
    internal static class SquareCollectionMoveExtensions
    {
        public static bool IsRightMovePossible(this IEnumerable<ISquare> squares)
        {
            var array = squares.ToArray();
            for (var i = array.Length - 1; i > 0; i--)
            {
                var square = array[i];
                var squareToTheLeft = array[i - 1];

                // empty tile in square, with tile in left square 
                if (!square.HasTile() && squareToTheLeft.HasTile())
                    return true;

                // merge-able tiles
                if (square.HasTile() && square.GetSafeTileValue() == squareToTheLeft.GetSafeTileValue())
                    return true;
            }
            return false;
        }
    }
}