﻿using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;
using Sharp48.Solvers.Extensions;
using System.Linq;
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
        [InlineData("2,2,,", " , , ,4")]
        [InlineData("2,,,2", " , , ,4")]
        [InlineData("2,2,,2", " , ,2,4")]
        [InlineData(",,,2", " , , ,2")]
        [InlineData(",,4,2", " , ,4,2")]
        public void MoveDownWorks(string input, string expected)
        {
            // Arrange
            var column = Column.Parse(input);
            uint score;

            // Act
            var moved = column.MakeMove(Move.Down, out score);
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
        public void MoveDownCollectsProperScore(string input, uint expectedScore)
        {
            // Arrange
            var column = Column.Parse(input);
            uint score;

            // Act
            column.MakeMove(Move.Down, out score);

            // Assert
            Assert.Equal(expectedScore, score);
        }

        [Theory]
        [InlineData("2,,,", "2, , , ")]
        [InlineData("2,2,,", "4, , , ")]
        [InlineData("2,,,2", "4, , , ")]
        [InlineData("2,2,,2", "4,2, , ")]
        [InlineData(",,,2", "2, , , ")]
        [InlineData(",,4,2", "4,2, , ")]
        public void MoveUpWorks(string input, string expected)
        {
            // Arrange
            var column = Column.Parse(input);
            uint score;

            // Act
            var moved = column.MakeMove(Move.Up, out score);
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
        public void MoveUpCollectsProperScore(string input, uint expectedScore)
        {
            // Arrange
            var column = Column.Parse(input);
            uint score;

            // Act
            column.MakeMove(Move.Up, out score);

            // Assert
            Assert.Equal(expectedScore, score);
        }
    }
}