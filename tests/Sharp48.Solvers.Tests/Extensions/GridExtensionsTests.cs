using Sharp48.Core;
using Sharp48.Core.PlayArea;
using Sharp48.Solvers.Extensions;
using Xunit;

namespace Sharp48.Solvers.Tests.Extensions
{
    public class GridExtensionsTests
    {
        [Theory]
        [InlineData((ushort) 0x0129, (ushort) 0x9210)]
        public void ReverseWorks(ushort input, ushort output)
        {
            // Act
            var reverse = input.Reverse();

            // Assert
            Assert.Equal(output, reverse);
        }

        [Theory]
        [InlineData(",,2,,4,,8,4,2,4,32,16,2,2,512,256", 0x0010203212541198UL)]
        public void GameAsGridWorks(string gridString, ulong expected)
        {
            // Arrange
            var grid = Grid.Parse(gridString);
            var game = new Game(grid, false, 0);

            // Act
            var actual = game.AsGrid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0x0010203212541198UL, (byte) 4)]
        public void EmptySquaresAreCountedProperly(ulong grid, byte expected)
        {
            // Act
            var actual = grid.EmptySquaresCount();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}