using System;
using Sharp48.Core.PlayArea;
using Xunit;

namespace Sharp48.Core.Tests
{
    public class GridTests
    {
        [Theory]
        [InlineData("2,,,,2,2,,,4,4,,,8,8,,", "2, , , :2,2, , :4,4, , :8,8, , ")]
        public void ParsingWorks(string input, string expected)
        {
            // Arrange
            expected = expected.Replace(":", Environment.NewLine);

            // Act
            var row = Grid.Parse(input);
            var actual = row.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}