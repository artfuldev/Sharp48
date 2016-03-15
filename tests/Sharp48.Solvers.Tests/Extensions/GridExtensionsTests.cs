using System;
using System.Linq;
using System.Linq.Expressions;
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

        [Theory]
        [InlineData((ushort) 0x0010, "0,0,1,0")]
        public void AsTilesWorks(ushort row, string tiles)
        {
            // Arrange
            var expected = tiles.Split(',').Select(x => Convert.ToByte(x)).ToArray();

            // Act
            var actual = row.AsTiles();

            // Assert
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(0x0010203212541198UL, (ushort)0x0010, (ushort)0x2032, (ushort)0x1254, (ushort)0x1198)]
        public void GetRowsWorks(ulong grid, ushort row1, ushort row2, ushort row3, ushort row4)
        {
            // Arrange
            var expected = new[] {row1, row2, row3, row4};

            // Act
            var actual = grid.GetRows();

            // Assert
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(0x0010203212541198UL, 0x0211002113590248UL)]
        public void TransposeWorks(ulong grid, ulong expected)
        {
            // Act
            var actual = grid.Transpose();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0x0010203212541198UL, (ushort)0x0211, (ushort)0x0021, (ushort)0x1359, (ushort)0x0248)]
        public void GetColumnsWorks(ulong grid, ushort column1, ushort column2, ushort column3, ushort column4)
        {
            // Arrange
            var expected = new[] { column1, column2, column3, column4 };

            // Act
            var actual = grid.GetColumns();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0x0010203212541198UL, 0x1010203212541198UL, 0x0110203212541198UL, 0x0011203212541198UL, 0x0010213212541198UL)]
        public void GetPossible2GenerationsWorks(ulong grid, ulong grid1, ulong grid2, ulong grid3, ulong grid4)
        {
            // Arrange
            var expected = new[] { grid1, grid2, grid3, grid4 }.Reverse();

            // Act
            var actual = grid.GetPossible2Generations();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0x0010203212541198UL, 0x2010203212541198UL, 0x0210203212541198UL, 0x0012203212541198UL, 0x0010223212541198UL)]
        public void GetPossible4GenerationsWorks(ulong grid, ulong grid1, ulong grid2, ulong grid3, ulong grid4)
        {
            // Arrange
            var expected = new[] { grid1, grid2, grid3, grid4 }.Reverse();

            // Act
            var actual = grid.GetPossible4Generations();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData((ushort)0x0010, (ushort)0x2032, (ushort)0x1254, (ushort)0x1198, 0x0010203212541198UL)]
        public void ToGridWorks(ushort row1, ushort row2, ushort row3, ushort row4, ulong expected)
        {
            // Arrange
            var rows = new[] { row1, row2, row3, row4 };

            // Act
            var actual = rows.ToGrid();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}