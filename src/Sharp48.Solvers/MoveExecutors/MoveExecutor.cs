using System.Collections.Generic;
using System.Linq;
using Sharp48.Core.Moves;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.MoveExecutors
{
    public class MoveExecutor : IMoveExecutor
    {
        private readonly IDictionary<ushort, IDictionary<Move,ushort>> _lookup;

        private IEnumerable<Move> GetPossibleMoves(ushort row)
        {
            if (_lookup[row][Move.Left] != 0)
                yield return Move.Left;
            if (_lookup[row][Move.Right] != 0)
                yield return Move.Right;
        }

        public IEnumerable<Move> GetPossibleMoves(ulong grid)
        {
            return
                grid.GetRows()
                    .SelectMany(GetPossibleMoves)
                    .Distinct()
                    .Concat(
                        grid.GetColumns()
                            .SelectMany(GetPossibleMoves)
                            .Select(x => x == Move.Left ? Move.Up : Move.Down)
                            .Distinct());
        }

        public ulong MakeMove(ulong grid, Move move)
        {
            return 0ul;
        }
    }
}