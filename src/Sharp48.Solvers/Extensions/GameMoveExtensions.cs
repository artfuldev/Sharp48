using System.Collections.Generic;
using Sharp48.Core;
using Sharp48.Core.Moves;
using System.Linq;

namespace Sharp48.Solvers.Extensions
{
    internal static class GameMoveExtensions
    {
        public static IEnumerable<Move> GetPossibleMoves(this IGame game) => game.Grid.GetPossibleMoves();

        public static IEnumerable<IGame> GetPossibleGenerations(this IGame game)
            => game.Grid.GetPossibleGenerations()
                .Select(grid => new Game(grid, !grid.GetPossibleMoves().Any(), game.Score));

        public static IEnumerable<IGame> GetPossible2Generations(this IGame game)
            => game.Grid.GetPossible2Generations()
                .Select(grid => new Game(grid, !grid.GetPossibleMoves().Any(), game.Score));

        public static IEnumerable<IGame> GetPossible4Generations(this IGame game)
            => game.Grid.GetPossible4Generations()
                .Select(grid => new Game(grid, !grid.GetPossibleMoves().Any(), game.Score));

        public static IGame MakeMove(this IGame game, Move move)
        {
            uint moveScore;
            var grid = game.Grid.MakeMove(move, out moveScore);
            var score = moveScore + game.Score;
            var over = !grid.GetPossibleMoves().Any();
            return new Game(grid, over, score);
        }
    }
}