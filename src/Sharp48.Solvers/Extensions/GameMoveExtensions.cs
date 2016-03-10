using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.Solvers.Extensions
{
    internal static class GameMoveExtensions
    {
        public static IEnumerable<Move> GetPossibleMoves(this IGame game)
        {
            return game.Grid.GetPossibleMoves();
        }

        public static IEnumerable<IGame> GetPossibleGenerations(this IGame game)
        {
            return game.Grid.GetPossibleGenerations().Select(grid =>
            {
                var over = !grid.GetPossibleMoves().Any();
                var score = game.Score;
                return new Game(grid, over, score);
            });
        }

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