using System.Collections.Generic;
using System.Linq;
using Sharp48.Core;
using Sharp48.Core.Moves;

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
            return game.Grid.GetPossibleGenerations().Select(x => new Game(x, !x.GetPossibleMoves().Any(), game.Score));
        } 
    }
}