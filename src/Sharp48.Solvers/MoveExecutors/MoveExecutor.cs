using System;
using System.Collections.Generic;
using System.Linq;
using Sharp48.Core.Moves;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.MoveExecutors
{
    public class MoveExecutor : IMoveExecutor
    {
        private readonly IDictionary<ushort, IDictionary<Move,ushort>> _lookup;

        public IEnumerable<Move> GetPossibleMoves(ushort row)
        {
            if (_lookup[row][Move.Left] != row)
                yield return Move.Left;
            if (_lookup[row][Move.Right] != row)
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

        public ushort MakeMove(ushort row, Move move)
        {
            switch (move)
            {
                case Move.Left:
                case Move.Right:
                    return _lookup[row][move];
                default:
                    return row;
            }
        }

        public ulong MakeMove(ulong grid, Move move)
        {
            switch (move)
            {
                case Move.Up:
                case Move.Down:
                    var lookupMove = move == Move.Up ? Move.Left : Move.Right;
                    return grid.Transpose().GetRows().Select(x => MakeMove(x, lookupMove)).ToArray().ToGrid().Transpose();
                case Move.Left:
                case Move.Right:
                    return grid.GetRows().Select(x => MakeMove(x, move)).ToArray().ToGrid();
                default:
                    return grid;
            }
        }
    }
}