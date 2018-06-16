namespace Rogozinski.Trading.Adx.Tests
{
    using Xunit;

    public class TrueRangeCalculatorTests
    {
        private TrueRangeCalculator sut;

        public TrueRangeCalculatorTests()
        {
            sut = new TrueRangeCalculator();
        }

        [Fact]
        public void CalculateShouldReturnDistanceBetweenCurrentHighAndLow()
        {
            var previousPrice = new PricePoint(4, 8, 2, 6);
            var currentPrice = new PricePoint(6, 12, 4, 10);

            var actual = sut.Calculate(previousPrice, currentPrice);

            Assert.Equal(8, actual);
        }

        [Fact]
        public void CalculateShouldReturnDistanceBetweenCurrentHighAndPreviousClose()
        {
            var previousPrice = new PricePoint(2, 6, 0, 4);
            var currentPrice = new PricePoint(10, 16, 12, 14);

            var actual = sut.Calculate(previousPrice, currentPrice);

            Assert.Equal(12, actual);
        }

        [Fact]
        public void CalculateShouldReturnDistanceBetweenCurrentLowAndPreviousClose()
        {
            var previousPrice = new PricePoint(14, 16, 10, 12);
            var currentPrice = new PricePoint(6, 8, 2, 4);

            var actual = sut.Calculate(previousPrice, currentPrice);

            Assert.Equal(10, actual);
        }
    }
}