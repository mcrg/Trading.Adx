using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Rogozinski.Trading.Adx.Tests
{
    public class AdxCalculatorIntegrationTests
    {
        private IAdxCalculator sut;

        public AdxCalculatorIntegrationTests()
        {
            var trueRangeCalculator = new TrueRangeCalculator();
            var dmCalculator = new DmCalculator();
            sut = new AdxCalculator(
                new DmPointCalculator(trueRangeCalculator, dmCalculator),
                new AdxPointCalculator(trueRangeCalculator, new AccumulationCalculator(), dmCalculator));
        }

        public static IEnumerable<object[]> PricePoints()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    GetPricePoints()
                }
            };
        }

        private static List<PricePoint> GetPricePoints()
        {
            return new List<PricePoint>()
            {
                new PricePoint(274, 272, 272.75),
                    new PricePoint(273.25, 270.25, 270.75),
                    new PricePoint(272, 269.75, 270),
                    new PricePoint(270.75, 268, 269.25),
                    new PricePoint(270, 269, 269.75),
                    new PricePoint(270.5, 268, 270),
                    new PricePoint(268.5, 266.5, 266.5),
                    new PricePoint(265.5, 263, 263.25),
                    new PricePoint(262.5, 259, 260.25),
                    new PricePoint(263.5, 260, 263),
                    new PricePoint(269.5, 263, 266.5),
                    new PricePoint(267.25, 265, 267),
                    new PricePoint(267.5, 265.5, 265.75),
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
                    new PricePoint(261, 255.5, 255.5),
                    new PricePoint(257.5, 253, 253),
                    new PricePoint(259, 254, 257.5),
                    new PricePoint(259.75, 257.5, 257.5),
                    new PricePoint(257.25, 250, 250),
                    new PricePoint(250, 247, 249.75),
                    new PricePoint(254.25, 252.75, 253.75),
                    new PricePoint(254, 250.5, 252.25),
                    new PricePoint(253.25, 250.25, 250.50),
                    new PricePoint(253.25, 251, 253),
                    new PricePoint(251.75, 250.5, 251.50),
                    new PricePoint(253, 249.5, 250),
                    new PricePoint(251.50, 245.25, 245.75),
                    new PricePoint(246.25, 240, 242.75),
                    new PricePoint(244.25, 244.25, 243.5)
            };
        }

        [Theory]
        [InlineData(0, 16, 57.05, 12.28, 21.75, 22, 38)]
        [InlineData(1, 17, 57.47, 11.40, 22.70, 20, 39)]
        [InlineData(2, 18, 59.36, 12.09, 21.08, 20, 36)]
        [InlineData(3, 18, 57.37, 11.98, 19.57, 21, 34)]
        [InlineData(4, 20, 60.77, 11.12, 25.67, 18, 42)]
        [InlineData(5, 22, 59.43, 10.33, 26.84, 17, 45)]
        [InlineData(6, 22, 59.68, 13.84, 24.92, 23, 42)]
        [InlineData(7, 23, 58.92, 12.85, 25.39, 22, 43)]
        [InlineData(8, 24, 57.71, 11.93, 23.83, 21, 41)]
        [InlineData(9, 25, 56.34, 11.08, 22.13, 20, 39)]
        [InlineData(10, 26, 54.82, 10.29, 21.05, 19, 38)]
        [InlineData(11, 26, 54.40, 10.80, 19.55, 20, 36)]
        [InlineData(12, 27, 56.76, 10.03, 22.40, 18, 39)]
        [InlineData(13, 28, 58.96, 9.31, 26.05, 16, 44)]
        [InlineData(14, 29, 56.25, 8.64, 24.19, 15, 43)]

        public void Calculate(int positionInResults, double adx, double tr14, double plusDm14, double minusDm14, double plusDi14, double minusDi14)
        {
            var pricePoints = GetPricePoints();
            var actual = sut.Calculate(pricePoints).ToList();

            Assert.Equal(15, actual.Count);

            AssertHelper.AdxPointsEqual(new AdxPoint()
            {
                Adx = adx,
                    Tr14 = tr14,
                    Dm14 = new DmResult(plusDm14, minusDm14),
                    Di14 = new DiResult(plusDi14, minusDi14)
            }, actual[positionInResults]);
        }
    }
}