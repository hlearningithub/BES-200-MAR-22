using System;
using Xunit;

namespace LibraryApiIntegrationTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(3,3);
        }

        [Theory]
        [InlineData(2,2,4)]
        [InlineData(5, 5, 10)]
        [InlineData(2, 3, 5)]
        public void CanAdd(int a , int b, int extpectedAns)
        {
            var ans = a + b;
            Assert.Equal(extpectedAns, ans);

        }
    }
}
