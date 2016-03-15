using Sharp48.Core.Moves;
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
    }
}