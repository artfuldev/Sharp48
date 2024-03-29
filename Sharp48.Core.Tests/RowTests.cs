﻿using Sharp48.Core.PlayArea;

namespace Sharp48.Core.Tests
{
    public class RowTests
    {
        [Theory]
        [InlineData("2,,,", "2, , , ")]
        public void ParsingWorks(string input, string expected)
        {
            // Arrange

            // Act
            var row = Row.Parse(input);
            var actual = row.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}