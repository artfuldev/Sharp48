using System;
using System.Linq;
using Sharp48.Core.Moves;
using Sharp48.Solvers.Extensions;
using Sharp48.Solvers.MoveExecutors;
using Xunit;

namespace Sharp48.Solvers.Tests.MoveExecutors
{
    public class MoveExecutorTests
    {
        private readonly MoveExecutor _executor = new MoveExecutor();

        [Theory]
        [InlineData((ushort)0x2011, (ushort)0x2200)]
        [InlineData((ushort)0x1905, (ushort)0x1950)]
        public void LeftMoveWorks(ushort row, ushort expected)
        {
            // Act
            var actual = _executor.MakeMove(row, Move.Left);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((ushort)0x2011, (ushort)0x0022)]
        [InlineData((ushort)0x1905, (ushort)0x0195)]
        public void RightMoveWorks(ushort row, ushort expected)
        {
            // Act
            var actual = _executor.MakeMove(row, Move.Right);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0x1120211589661006ul, "Up,Right,Down,Left")]
        [InlineData(0x1243211589471666ul, "Right,Left")]
        public void GetPossibleMovesWorks(ulong grid, string moves)
        {
            // Arrange
            Move move;
            var expected = moves.Split(',').Select(x => Enum.TryParse(x, out move) ? move : Move.Up).OrderBy(x => x);

            // Act
            var actual = _executor.GetPossibleMoves(grid).OrderBy(x => x);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}