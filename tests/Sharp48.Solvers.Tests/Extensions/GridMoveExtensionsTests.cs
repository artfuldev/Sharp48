using System.Linq;
using Sharp48.Core.PlayArea;
using Sharp48.Solvers.Extensions;
using Xunit;

namespace Sharp48.Solvers.Tests.Extensions
{
    public class GridMoveExtensionsTests
    {
        [Theory]
        [InlineData(" ,2, ,2, , , , , , , , , , , , ", "Right,Down,Left")]
        public void GetPossibleMovesWorks(string input, string expected)
        {
            // Arrange
            var grid = Grid.Parse(input);

            // Act
            var possibleMoves = grid.GetPossibleMoves();
            var actual = string.Join(",", possibleMoves);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}