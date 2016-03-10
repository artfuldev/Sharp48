using System.Collections.Generic;
using System.Linq;
using Sharp48.Core.PlayArea;
using Sharp48.Core.Tiles;

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

        public static IEnumerable<ISquare> SlideRight(this IEnumerable<ISquare> squares)
        {
            var source = squares.Select(x => x.GetSafeTileValue()).ToArray();
            for (var i = source.Length - 1; i > 0; i--)
            {
                var number = source[i];
                var leftIndex = i - 1;
                var numberToTheLeft = source[leftIndex];
                if (number != 0)
                    continue;
                while (numberToTheLeft == 0 && leftIndex > 0)
                {
                    leftIndex--;
                    numberToTheLeft = source[leftIndex];
                }
                if (numberToTheLeft == 0)
                    continue;
                source[i] = numberToTheLeft;
                source[leftIndex] = 0;
            }
            return source.Select(x => new Square() {Tile = x == 0 ? null : new Tile() {Value = x}});
        }

        public static IEnumerable<ISquare> MergeRight(this IEnumerable<ISquare> squares, out uint score)
        {
            score = 0;
            var source = squares.Select(x => x.GetSafeTileValue()).ToArray();
            for (var i = source.Length - 1; i > 0; i--)
            {
                var number = source[i];
                if (number == 0)
                    continue;
                var numberToTheLeft = source[i - 1];
                if (numberToTheLeft != number)
                    continue;
                source[i] = 2*number;
                source[i - 1] = 0;
            }
            return source.Select(x => new Square() { Tile = x == 0 ? null : new Tile() { Value = x } });
        }

        public static IEnumerable<ISquare> MoveRight(this IEnumerable<ISquare> squares, out uint score)
        {
            return squares.SlideRight().MergeRight(out score).SlideRight();
        } 
    }
}