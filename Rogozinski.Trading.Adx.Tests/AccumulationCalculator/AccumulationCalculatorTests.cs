namespace Rogozinski.Trading.Adx.Tests
{
    using Xunit;
    
    public class AccumulationCalculatorTests
    {
        private AccumulationCalculator sut;

        public AccumulationCalculatorTests()
        {
            sut = new AccumulationCalculator();
        }

        [Theory]
        [InlineData(43.32, 41, 5.25)]
        [InlineData(42.98, 43.32, 2.75)]
        [InlineData(43.91, 42.98, 4)]
        [InlineData(8.82, 9.5, 0)]
        [InlineData(8.19, 8.82, 0)]
        [InlineData(11.6, 8.19, 4)]
        public void GetCurrentAccumulatedValueShouldReturnNextAccumulatedValue(double expected, double previousAccumulatedValue, double currentValue)
        {
            var actual = sut.GetCurrentAccumulatedValue(previousAccumulatedValue, currentValue);

            Assert.Equal(expected, actual);
        }
    }
}