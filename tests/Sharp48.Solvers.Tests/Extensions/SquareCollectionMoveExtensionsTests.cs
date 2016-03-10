using System.Linq;
using Sharp48.Core.PlayArea;
using Sharp48.Solvers.Extensions;
using Xunit;

namespace Sharp48.Solvers.Tests.Extensions
{
    public class SquareCollectionMoveExtensionsTests
    {

        [Theory]
        [InlineData("2,,,", true)]
        [InlineData("2,2,,", true)]
        [InlineData("2,,,2", true)]
        [InlineData("2,2,,2", true)]
        [InlineData(",,,2", false)]
        [InlineData(",,4,2", false)]
        public void RightMoveAvailabilityWorks(string row, bool expected)
        {
            // Arrange
            var squares = Row.Parse(row);

            // Act
            var actual = squares.IsRightMovePossible();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2,,,", " , , ,2")]
        [InlineData("2,2,,", " , ,2,2")]
        [InlineData("2,,,2", " , ,2,2")]
        [InlineData("2,2,,2", " ,2,2,2")]
        [InlineData(",,,2", " , , ,2")]
        [InlineData(",,4,2", " , ,4,2")]
        public void SlideRightWorks(string input, string expected)
        {
            // Arrange
            var squares = Row.Parse(input);

            // Act
            var slided = new Row(squares.SlideRight().ToList());
            var actual = slided.ToString();

            // Assert
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData("2,,,", "2, , , ")]
        [InlineData("2,2,,", " ,4, , ")]
        [InlineData("2,,,2", "2, , ,2")]
        [InlineData("2,2,,2", " ,4, ,2")]
        [InlineData(",,,2", " , , ,2")]
        [InlineData(",,4,2", " , ,4,2")]
        public void MergeRightWorks(string input, string expected)
        {
            // Arrange
            var squares = Row.Parse(input);
            uint score;

            // Act
            var merged = new Row(squares.MergeRight(out score).ToList());
            var actual = merged.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2,,,", 0U)]
        [InlineData("2,2,,", 4U)]
        [InlineData("2,,,2", 0U)]
        [InlineData("2,2,,2", 4U)]
        [InlineData(",,,2", 0U)]
        [InlineData(",,4,2", 0U)]
        [InlineData(",,4,4", 8U)]
        [InlineData("2,2,4,4", 12U)]
        public void MergeRightCollectsProperScore(string input, uint expectedScore)
        {
            // Arrange
            var squares = Row.Parse(input);
            uint score;

            // Act
            squares.MergeRight(out score);

            // Assert
            Assert.Equal(expectedScore, score);
        }

        [Theory]
        [InlineData("2,,,", " , , ,2")]
        [InlineData("2,2,,", " , , ,4")]
        [InlineData("2,,,2", " , , ,4")]
        [InlineData("2,2,,2", " , ,2,4")]
        [InlineData(",,,2", " , , ,2")]
        [InlineData(",,4,2", " , ,4,2")]
        public void MoveRightWorks(string input, string expected)
        {
            // Arrange
            var squares = Row.Parse(input);
            uint score;

            // Act
            var moved = new Row(squares.MoveRight(out score).ToList());
            var actual = moved.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2,,,", 0U)]
        [InlineData("2,2,,", 4U)]
        [InlineData("2,,,2", 4U)]
        [InlineData("2,2,,2", 4U)]
        [InlineData(",,,2", 0U)]
        [InlineData(",,4,2", 0U)]
        [InlineData(",,4,4", 8U)]
        [InlineData("2,2,4,4", 12U)]
        public void MoveRightCollectsProperScore(string input, uint expectedScore)
        {
            // Arrange
            var squares = Row.Parse(input);
            uint score;

            // Act
            squares.MoveRight(out score);

            // Assert
            Assert.Equal(expectedScore, score);
        }
    }
}