using System;
using System.Linq;
using Sharp48.Core.Moves;
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

        [Theory]
        [InlineData("4,16,8,4,32,64,4,2,2,8,2,2,2,4,2,",
            "4,16,8,4,32,64,4,2,2,8,2,2,2,4,2,2|4,16,8,4,32,64,4,2,2,8,2,2,2,4,2,4")]
        [InlineData("4,16,8,4,32,,4,2,2,8,2,2,2,4,2,",
            "4,16,8,4,32,2,4,2,2,8,2,2,2,4,2, |4,16,8,4,32,4,4,2,2,8,2,2,2,4,2, |4,16,8,4,32, ,4,2,2,8,2,2,2,4,2,2|4,16,8,4,32, ,4,2,2,8,2,2,2,4,2,4")]
        public void GetGenerationsWorks(string input, string expected)
        {
            // Arrange
            var grid = Grid.Parse(input);
            expected = string.Join("|", expected.Split('|').OrderBy(x => x));

            // Act
            var generations =
                grid.GetPossibleGenerations()
                    .Select(x => x.ToString().Replace(Environment.NewLine, ","))
                    .OrderBy(x => x).ToList();
            var actual = string.Join("|", generations);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("4,16,8,4,32,64,4,2,2,8,2,2,2,4,2, ",Move.Up, "4,16,8,4,32,64,4,4,4,8,4, , ,4, , ")]
        public void MakeMoveWorks(string input, Move move, string expected)
        {
            // Arrange
            var grid = Grid.Parse(input);
            uint score;

            // Act
            var result = grid.MakeMove(move, out score);
            var actual = result.ToString().Replace(Environment.NewLine, ",");

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("4,16,8,4,32,64,4,2,2,8,2,2,2,4,2, ", Move.Up, 12U)]
        public void MakeMoveScoresCorrectly(string input, Move move, uint expected)
        {
            // Arrange
            var grid = Grid.Parse(input);
            uint score;

            // Act
            grid.MakeMove(move, out score);

            // Assert
            Assert.Equal(expected, score);
        }
    }
}