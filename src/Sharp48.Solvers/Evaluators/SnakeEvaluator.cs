using System.Collections.Generic;
using Sharp48.Core;
using System.Linq;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Evaluators
{
    public class SnakeEvaluator : IEvaluator
    {
        private static readonly IEnumerable<int[]> MonotonicityPaths = new[]
           {
            new[] {0, 1, 2, 3, 7, 6, 5, 4, 8, 9, 10, 11, 15, 14, 13, 12},
            new[] {0, 4, 8, 12, 13, 9, 5, 1, 2, 6, 10, 14, 15, 11, 7, 3},
            new[] {3, 2, 1, 0, 4, 5, 6, 7, 11, 10, 9, 8, 12, 13, 14, 15},
            new[] {3, 7, 11, 15, 14, 10, 6, 2, 1, 5, 9, 13, 12, 8, 4, 0},
            new[] {12, 8, 4, 0, 1, 5, 9, 13, 2, 6, 10, 14, 15, 11, 7, 3},
            new[] {12, 13, 14, 15, 11, 10, 9, 8, 4, 5, 6, 7, 3, 2, 1, 0},
            new[] {15, 11, 7, 3, 2, 6, 10, 14, 13, 9, 5, 1, 0, 4, 8, 12},
            new[] {15, 14, 13, 12, 8, 9, 10, 11, 7, 6, 5, 4, 0, 1, 2, 3}
        };

        private static bool IsMonotonicallyDecreasing(IEnumerable<uint> tiles)
        {
            var array = tiles as uint[] ?? tiles.ToArray();
            for (var i = 0; i < array.Length - 1; i++)
                if (array[i] != 0 && array[i + 1] != 0 && array[i + 1] < array[i])
                    return false;
            return true;
        }

        public double Evaluate(IGame game)
        {
            return
                MonotonicityPaths.Select(
                    monotonicityPath => monotonicityPath.Select(t => game.Grid.Squares.ElementAt(t).GetSafeTileValue()))
                    .Select(tiles => tiles as uint[] ?? tiles.ToArray())
                    .Where(tiles => IsMonotonicallyDecreasing(tiles) || IsMonotonicallyDecreasing(tiles.Reverse()))
                    .Aggregate(0d, (current, tiles) => current + tiles.Sum(x => x));
        }
    }
}