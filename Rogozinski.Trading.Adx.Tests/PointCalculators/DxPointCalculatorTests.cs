namespace Rogozinski.Trading.Adx.Tests
{
    using System.Collections.Generic;
    using System;
    using Moq;
    using Xunit;

    public class DxPointCalculatorTests
    {
        private DxPointCalculator sut;

        public DxPointCalculatorTests()
        {
            sut = new DxPointCalculator(new TrueRangeCalculator(), new AccumulationCalculator(), new DmCalculator());
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
            sut = new DxPointCalculator(new TrueRangeCalculator(), new AccumulationCalculator(), new DmCalculator());

            var actual = sut.Calculate(dmPoint, pricePoints);

            Assert.Equal(16, actual.Adx);
            Assert.Equal(22, actual.Di14.PlusDi);
            Assert.Equal(38, actual.Di14.MinusDi);
            Assert.Equal(12.28, actual.Dm14.PlusDm);
            Assert.Equal(21.75, actual.Dm14.MinusDm);
            Assert.Equal(57.05, actual.Tr14);
        }
    }
}