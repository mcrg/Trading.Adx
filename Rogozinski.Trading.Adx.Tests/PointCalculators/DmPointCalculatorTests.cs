namespace Rogozinski.Trading.Adx.Tests
{
    using System.Collections.Generic;
    using System;
    using Moq;
    using Xunit;

    public class DmPointCalculatorTests
    {
        private Mock<ITrueRangeCalculator> trueRangeCalculatorMock;
        private Mock<IDmCalculator> dmCalculatorMock;
        private DmPointCalculator sut;

        public DmPointCalculatorTests()
        {
            trueRangeCalculatorMock = new Mock<ITrueRangeCalculator>();
            dmCalculatorMock = new Mock<IDmCalculator>();
            dmCalculatorMock.SetReturnsDefault(new DmResult(0, 0));
            sut = new DmPointCalculator(trueRangeCalculatorMock.Object, dmCalculatorMock.Object);
        }

        public static IEnumerable<object[]> PricePoints()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    new List<PricePoint>()
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
                        new PricePoint(269.75, 266, 268.5)
                    }
                }
            };
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void CalculateShouldThrowWhenListShorterThan2(int listSize)
        {
            var pricePoints = new List<PricePoint>();
            for (int i = 0; i < listSize; i++)
            {
                pricePoints.Add(new PricePoint(1, 2, 3));
            }

            Action action = ()=> sut.Calculate(pricePoints);

            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [MemberData(nameof(PricePoints))]
        public void CalculateShouldApplyTrueRangeCalculatorToEachPricePointPair(List<PricePoint> pricePoints)
        {
            sut.Calculate(pricePoints);

            trueRangeCalculatorMock.Verify(a => a.Calculate(It.IsAny<PricePoint>(), It.IsAny<PricePoint>()), Times.Exactly(pricePoints.Count - 1));
        }

        [Theory]
        [MemberData(nameof(PricePoints))]
        public void CalculateShouldApplyDmCalculatorToEachPricePointPair(List<PricePoint> pricePoints)
        {
            sut.Calculate(pricePoints);

            dmCalculatorMock.Verify(a => a.Calculate(It.IsAny<PricePoint>(), It.IsAny<PricePoint>()), Times.Exactly(pricePoints.Count - 1));
        }

        [Theory]
        [MemberData(nameof(PricePoints))]
        public void CalculateShouldProduceCorrectResults(List<PricePoint> pricePoints)
        {
            sut = new DmPointCalculator(new TrueRangeCalculator(), new DmCalculator());

            var actual = sut.Calculate(pricePoints);

            Assert.Equal(41, actual.Tr14);
            Assert.Equal(9.5, actual.Dm14.PlusDm);
            Assert.Equal(14, actual.Dm14.MinusDm);
        }
    }
}