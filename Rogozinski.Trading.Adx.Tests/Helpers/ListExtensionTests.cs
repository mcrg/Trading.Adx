using System.Collections.Generic;
using Xunit;

namespace Rogozinski.Trading.Adx.Tests
{
    public class ListExtensionTests
    {
        private List<string> testData = new List<string>() { "a", "b", "c", "d", "e", "f" };

        [Fact]
        public void SliceShouldReturnTheSameListWhenStartIndexIs0AndNoEndGiven()
        {
            var actual = testData.Slice(0);
            Assert.Equal(testData, actual);
        }

        [Fact]
        public void SliceShouldReturnOnlyFirstElement()
        {
            var actual = testData.Slice(0, 0);
            Assert.Equal(new List<string>() { testData[0] }, actual);
        }

        [Fact]
        public void SliceShouldReturnOnlyFirstTwoElements()
        {
            var actual = testData.Slice(0, 1);
            Assert.Equal(new List<string>() { testData[0], testData[1] }, actual);
        }

        [Fact]
        public void SliceShouldReturnOnlyLastElement()
        {
            var actual = testData.Slice(5, 5);
            Assert.Equal(new List<string>() { testData[5] }, actual);
        }

        [Fact]
        public void SliceShouldReturnOnlyLastTwoElements()
        {
            var actual = testData.Slice(4, 5);
            Assert.Equal(new List<string>() { testData[4], testData[5] }, actual);
        }

        [Fact]
        public void SliceShouldReturnListEndingPartWhenStartIndexInTheMiddleAndNoEndGiven()
        {
            var actual = testData.Slice(2);
            Assert.Equal(new List<string>() { testData[2], testData[3], testData[4], testData[5] }, actual);
        }

        [Fact]
        public void SliceShouldReturnListMiddlePartWhenStartAndEndIndexInTheMiddle()
        {
            var actual = testData.Slice(1, 3);
            Assert.Equal(new List<string>() { testData[1], testData[2], testData[3] }, actual);
        }
    }
}