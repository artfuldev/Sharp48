using System;
using Xunit;
using Sharp48.Solvers.Extensions;

namespace Sharp48.Solvers.Tests.Extensions
{
    public class GridExtensionsTests
    {
        [Theory]
        [InlineData((ushort)0x0129, (ushort)0x9210)]
        public void ReverseWorks(ushort input, ushort output)
        {
            // Act
            var reverse = input.Reverse();

            // Assert
            Assert.Equal(output, reverse);
        } 
    }
}