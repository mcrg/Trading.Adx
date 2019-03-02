namespace Rogozinski.Trading.Adx.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Moq;
    using Xunit;

    public class AdxPointCalculatorTests
    {
        private AdxPointCalculator sut;

        public AdxPointCalculatorTests()
        {
            sut = new AdxPointCalculator(new TrueRangeCalculator(), new AccumulationCalculator(), new DmCalculator());
        }

        public static IEnumerable<object[]> PricePoints()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    new DmPointResult(41, 9.5, 14),
                        new List<PricePoint>()
                        {
                            new PricePoint(269.75, 266, 268.5),
                                new PricePoint(268.25, 263.25, 264.25),
                                new PricePoint(264, 261.5, 264),
                                new PricePoint(268, 266.25, 266.5),
                                new PricePoint(266, 264.25, 265.25),
                                new PricePoint(274, 267, 273),
                                new PricePoint(277.5, 273.5, 276.75),
                                new PricePoint(277, 272.5, 273),
                                new PricePoint(272, 269.5, 270.25),
                                new PricePoint(267.75, 264, 266.75),
                                new PricePoint(269.25, 263, 263),
                                new PricePoint(266, 263.5, 265.5),
                                new PricePoint(265, 262, 262.25),
                                new PricePoint(264.75, 261.5, 262.75),
                                new PricePoint(261, 255.5, 255.5)
                        }
                }
            };
        }

        [Fact]
        public void CalculateShouldThrowWhenListEmpty()
        {
            var pricePoints = new List<PricePoint>();

            Action action = () => sut.Calculate(new DmPointResult(0, 0, 0), pricePoints);

            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [MemberData(nameof(PricePoints))]
        public void ProduceCorrectResults(DmPointResult dmPoint, List<PricePoint> pricePoints)
        {
            var actual = sut.Calculate(dmPoint, pricePoints);

            AssertHelper.AdxPointsEqual(new AdxPoint()
            {
                Adx = 16,
                    Tr14 = 57.05,
                    Dm14 = new DmResult(12.28, 21.75),
                    Di14 = new DiResult(22, 38),
                    Price = pricePoints.Last()
            }, actual);
        }

        [Fact]
        public void CalculateWhenPrevAdx22()
        {
            var prevAdx = new AdxPoint()
            {
                Price = new PricePoint() { High = 250, Low = 247, Close = 249.75 },
                Tr14 = 59.43,
                Dm14 = new DmResult(10.33, 26.84),
                Di14 = new DiResult(17, 45),
                Adx = 22
            };
            var currentPrice = new PricePoint() { High = 254.25, Low = 252.75, Close = 253.75 };
            var actual = sut.Calculate(prevAdx, currentPrice);

            Assert.NotNull(actual);
            AssertHelper.AdxPointsEqual(new AdxPoint()
            {
                Adx = 22,
                    Tr14 = 59.68,
                    Dm14 = new DmResult(13.84, 24.92),
                    Di14 = new DiResult(23, 42),
                    Price = currentPrice
            }, actual);
        }

        [Fact]
        public void CalculateWhenPrevAdx24()
        {
            var prevAdx = new AdxPoint()
            {
                Price = new PricePoint() { High = 253.25, Low = 251, Close = 253 },
                Tr14 = 56.34,
                Dm14 = new DmResult(11.08, 22.13),
                Di14 = new DiResult(20, 39),
                Adx = 24
            };
            var currentPrice = new PricePoint() { High = 251.75, Low = 250.50, Close = 251.50 };
            var actual = sut.Calculate(prevAdx, currentPrice);

            Assert.NotNull(actual);
            AssertHelper.AdxPointsEqual(new AdxPoint()
            {
                Adx = 25,
                    Tr14 = 54.82,
                    Dm14 = new DmResult(10.29, 21.05),
                    Di14 = new DiResult(19, 38),
                    Price = currentPrice
            }, actual);
        }

        [Fact]
        public void CalculateWhenPrevAdx28()
        {
            var prevAdx = new AdxPoint()
            {
                Price = new PricePoint() { High = 246.25, Low = 240, Close = 242.75 },
                Tr14 = 58.96,
                Dm14 = new DmResult(9.31, 26.05),
                Di14 = new DiResult(16, 44),
                Adx = 28
            };
            var currentPrice = new PricePoint() { High = 244.25, Low = 244.25, Close = 243.50 };
            var actual = sut.Calculate(prevAdx, currentPrice);

            Assert.NotNull(actual);
            Assert.Same(currentPrice, actual.Price);
            Assert.Equal(29, actual.Adx);
            Assert.Equal(56.25, actual.Tr14);
            Assert.Equal(8.64, actual.Dm14.PlusDm);
            Assert.Equal(24.19, actual.Dm14.MinusDm);
            Assert.Equal(15, actual.Di14.PlusDi);
            Assert.Equal(43, actual.Di14.MinusDi);
        }

        [Fact]
        public void CalculateWhenPrevAdx17()
        {
            var prevAdx = new AdxPoint()
            {
                Price = new PricePoint() { High = 257.50, Low = 253, Close = 253 },
                Tr14 = 57.47,
                Dm14 = new DmResult(11.40, 22.70),
                Di14 = new DiResult(20, 39),
                Adx = 17
            };
            var currentPrice = new PricePoint() { High = 259, Low = 254, Close = 257.50 };
            var actual = sut.Calculate(prevAdx, currentPrice);

            Assert.NotNull(actual);
            AssertHelper.AdxPointsEqual(new AdxPoint()
            {
                Adx = 18,
                    Tr14 = 59.36,
                    Dm14 = new DmResult(12.09, 21.08),
                    Di14 = new DiResult(20, 36),
                    Price = currentPrice
            }, actual);
        }
    }
}