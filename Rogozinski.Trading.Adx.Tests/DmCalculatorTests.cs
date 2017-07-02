namespace Rogozinski.Trading.Adx.Tests
{
    using System.Collections.Generic;
    using Xunit;

    public class DmCalculatorTests
    {
        [Fact]
        public void CalculateShouldReturnPlusDmWhenMovementUp()
        {
            var sut = new DmCalculator();
            var previousPrice = new PricePoint(2, 8, 2, 8);
            var currentPrice = new PricePoint(4, 12, 4, 12);

            var actual = sut.Calculate(previousPrice, currentPrice);

            AssertPlusDm(4, actual);
        }

        [Fact]
        public void CalculateShouldReturnMinusDmWhenMovementDown()
        {
            var sut = new DmCalculator();
            var previousPrice = new PricePoint(12, 12, 6, 6);
            var currentPrice = new PricePoint(8, 8, 2, 2);

            var actual = sut.Calculate(previousPrice, currentPrice);

            AssertMinusDm(4, actual);
        }

        [Fact]
        public void CalculateShouldReturnPlusDmWhenMovementOutsideUp()
        {
            var sut = new DmCalculator();
            var previousPrice = new PricePoint(4, 6, 4, 6);
            var currentPrice = new PricePoint(2, 12, 2, 12);

            var actual = sut.Calculate(previousPrice, currentPrice);

            AssertPlusDm(6, actual);
        }

        [Fact]
        public void CalculateShouldReturnMinusDmWhenMovementOutsideUp()
        {
            var sut = new DmCalculator();
            var previousPrice = new PricePoint(10, 10, 8, 8);
            var currentPrice = new PricePoint(12, 12, 2, 2);

            var actual = sut.Calculate(previousPrice, currentPrice);

            AssertMinusDm(6, actual);
        }

        public static IEnumerable<object> InsideMovement()
        {
            return new List<object>()
            {
                new object[]{ new PricePoint(2, 12, 2, 12), new PricePoint(4, 8, 4, 8) },
                new object[]{ new PricePoint(2, 12, 2, 12), new PricePoint(2, 12, 2, 12) }
            };
        }

        [Theory]
        [MemberData("InsideMovement")]
        public void CalculateShouldReturnZeroDmWhenMovementInside(IPricePoint previousPrice, IPricePoint currentPrice)
        {
            var sut = new DmCalculator();

            var actual = sut.Calculate(previousPrice, currentPrice);

            AssertZeroDm(actual);
        }

        public static IEnumerable<object> LimitUp()
        {
            return new List<object>()
            {
                new object[]{ new PricePoint(2, 4, 2, 4), new PricePoint(8, 8, 8, 8), 4},
                new object[]{ new PricePoint(2, 4, 2, 4), new PricePoint(8, 12, 8, 12), 8 }
            };
        }

        [Theory]
        [MemberData("LimitUp")]
        public void CalculateShouldReturnPositiveDmWhenLimitUp(IPricePoint previousPrice, IPricePoint currentPrice, double expected)
        {
            var sut = new DmCalculator();

            var actual = sut.Calculate(previousPrice, currentPrice);

            AssertPlusDm(expected, actual);
        }

        public static IEnumerable<object> LimitDown()
        {
            return new List<object>()
            {
                new object[]{ new PricePoint(8, 8, 8, 8), new PricePoint(4, 4, 4, 4), 4},
                new object[]{ new PricePoint(12, 12, 8, 8), new PricePoint(4, 4, 4, 4), 4},
                new object[]{ new PricePoint(12, 12, 8, 8), new PricePoint(4, 4, 2, 2), 6},
            };
        }

        [Theory]
        [MemberData("LimitDown")]
        public void CalculateShouldReturnNegativeDmWhenLimitDown(IPricePoint previousPrice, IPricePoint currentPrice, double expected)
        {
            var sut = new DmCalculator();

            var actual = sut.Calculate(previousPrice, currentPrice);

            AssertMinusDm(expected, actual);
        }

        private static void AssertPlusDm(double plusDm, DmResult actual)
        {
            Assert.Equal(plusDm, actual.PlusDm);
            Assert.Equal(0, actual.MinusDm);
        }

        private static void AssertMinusDm(double minusDm, DmResult actual)
        {
            Assert.Equal(0, actual.PlusDm);
            Assert.Equal(minusDm, actual.MinusDm);
        }

        private static void AssertZeroDm(DmResult actual)
        {
            Assert.Equal(0, actual.PlusDm);
            Assert.Equal(0, actual.MinusDm);
        }
    }
}
