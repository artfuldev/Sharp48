using Sharp48.Core.PlayArea;
using Xunit;

namespace Sharp48.Core.Tests
{
    public class ColumnTests
    {
        [Theory]
        [InlineData("2,,,", "2, , , ")]
        public void ParsingWorks(string input, string expected)
        {
            // Arrange

            // Act
            var row = Column.Parse(input);
            var actual = row.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}