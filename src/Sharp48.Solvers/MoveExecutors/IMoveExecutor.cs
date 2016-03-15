using System.Collections.Generic;
using Sharp48.Core.Moves;

namespace Sharp48.Solvers.MoveExecutors
{
    public interface IMoveExecutor
    {
        IEnumerable<Move> GetPossibleMoves(ulong grid); 
        ulong MakeMove(ulong grid, Move move);
    }
}