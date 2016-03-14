using System.Collections.Generic;
using Sharp48.Core.Moves;

namespace Sharp48.Solvers.MoveExecutors
{
    public class MoveExecutor : IMoveExecutor
    {
        private readonly IDictionary<Move,IDictionary<ushort,ushort>> _lookup; 
        public ulong MakeMove(ulong grid, Move move)
        {
            return 0ul;
        }
    }
}