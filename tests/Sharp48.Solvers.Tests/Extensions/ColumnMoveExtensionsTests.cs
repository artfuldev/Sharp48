﻿using System.Linq;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;
using Sharp48.Solvers.Extensions;
using Xunit;

namespace Sharp48.Solvers.Tests.Extensions
{
    public class ColumnMoveExtensionsTests
    {
        [Theory]
        [InlineData("2,,,", true)]
        [InlineData("2,2,,", true)]
        [InlineData("2,,,2", true)]
        [InlineData("2,2,,2", true)]
        [InlineData(",,,2", false)]
        [InlineData(",,4,2", false)]
        public void RightMoveAvailabilityWorks(string column, bool expected)
        {
            // Arrange
            var squares = Column.Parse(column);

            // Act
            var actual = squares.IsRightMovePossible();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2,,,", true)]
        [InlineData("2,2,,", true)]
        [InlineData("2,,,2", true)]
        [InlineData("2,2,,2", true)]
        [InlineData(",,,2", false)]
        [InlineData(",,4,2", false)]
        public void DownMoveExists(string column, bool exists)
        {
            // Arrange
            var squares = Column.Parse(column);

            // Act
            var actual = squares.GetPossibleMoves().Contains(Move.Down);

            // Assert
            Assert.Equal(exists, actual);
        }

        [Theory]
        [InlineData("2,,,", false)]
        [InlineData("2,2,,", true)]
        [InlineData("2,,,2", true)]
        [InlineData("2,2,,2", true)]
        [InlineData(",,,2", true)]
        [InlineData(",,4,2", true)]
        public void UpMoveExists(string column, bool exists)
        {
            // Arrange
            var squares = Column.Parse(column);

            // Act
            var actual = squares.GetPossibleMoves().Contains(Move.Up);

            // Assert
            Assert.Equal(exists, actual);
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
            var squares = Column.Parse(input);

            // Act
            var slided = new Column(squares.SlideRight().ToList());
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
            var squares = Column.Parse(input);
            uint score;

            // Act
            var slided = new Column(squares.MergeRight(out score).ToList());
            var actual = slided.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}