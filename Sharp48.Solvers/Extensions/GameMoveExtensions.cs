using Sharp48.Core;
using Sharp48.Core.Moves;

namespace Sharp48.Solvers.Extensions
{
    internal static class GameMoveExtensions
    {
        public static IEnumerable<Move> GetPossibleMoves(this IGame game) => game.Grid.GetPossibleMoves();

        public static IEnumerable<IGame> GetPossibleGenerations(this IGame game)
            => game.Grid.GetPossibleGenerations()
                .Select(grid => new Game(grid, !grid.GetPossibleMoves().Any()));

        public static IEnumerable<IGame> GetPossible2Generations(this IGame game)
            => game.Grid.GetPossible2Generations()
                .Select(grid => new Game(grid, !grid.GetPossibleMoves().Any()));

        public static IEnumerable<IGame> GetPossible4Generations(this IGame game)
            => game.Grid.GetPossible4Generations()
                .Select(grid => new Game(grid, !grid.GetPossibleMoves().Any()));

        public static IGame MakeMove(this IGame game, Move move)
        {
            var grid = game.Grid.MakeMove(move);
            var over = !grid.GetPossibleMoves().Any();
            return new Game(grid, over);
        }
    }
}