using System.Collections.Generic;
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
    }
}