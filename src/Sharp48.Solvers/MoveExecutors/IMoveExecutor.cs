using Sharp48.Core.Moves;

namespace Sharp48.Solvers.MoveExecutors
{
    public interface IMoveExecutor
    {
        ulong MakeMove(ulong grid, Move move);
    }
}