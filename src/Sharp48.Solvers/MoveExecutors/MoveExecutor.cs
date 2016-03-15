using System;
using System.Collections.Generic;
using System.Linq;
using Facet.Combinatorics;
using Sharp48.Core.Moves;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.MoveExecutors
{
    public class MoveExecutor : IMoveExecutor
    {
        private readonly IDictionary<ushort, IDictionary<Move,ushort>> _lookup = new Dictionary<ushort, IDictionary<Move, ushort>>();

        public MoveExecutor()
        {
            var values = Enumerable.Range(0, 15).Select(x => (byte)x).ToArray();
            var rows = new Variations<byte>(values, 4, GenerateOption.WithRepetition);
            foreach (var row in rows)
            {
                var copy = row.ToArray().ToRow();
                // execute a move to the left
                for (var i = 0; i < 3; ++i)
                {
                    int j;
                    for (j = i + 1; j < 4; ++j)
                    {
                        if (row[j] != 0) break;
                    }
                    if (j == 4) break; // no more tiles to the right

                    if (row[i] == 0)
                    {
                        row[i] = row[j];
                        row[j] = 0;
                        i--; // retry this entry
                    }
                    else if (row[i] == row[j])
                    {
                        if (row[i] != 0xf)
                        {
                            /* Pretend that 32768 + 32768 = 32768 (representational limit). */
                            row[i]++;
                        }
                        row[j] = 0;
                    }
                }
                var leftMoveResult = (ushort) ((row[0] << 0) |
                                               (row[1] << 4) |
                                               (row[2] << 8) |
                                               (row[3] << 12));
                var rightMoveResult = leftMoveResult.Reverse();
                if(!_lookup.ContainsKey(copy))
                    _lookup[copy] = new Dictionary<Move, ushort>();
                var reverse = copy.Reverse();
                if (!_lookup.ContainsKey(reverse))
                    _lookup[reverse] = new Dictionary<Move, ushort>();
                _lookup[copy][Move.Left] = leftMoveResult;
                _lookup[copy][Move.Right] = rightMoveResult;
                _lookup[reverse][Move.Left] = rightMoveResult;
                _lookup[reverse][Move.Right] = leftMoveResult;
            }
        }

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