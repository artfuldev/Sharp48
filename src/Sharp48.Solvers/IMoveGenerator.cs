using System.Collections.Generic;
using Sharp48.Core;
using Sharp48.Core.Moves;

namespace Sharp48.Solvers
{
    public interface IMoveGenerator
    {
        IEnumerable<Move> GetPossibleMoves(IGame game);
    }
}