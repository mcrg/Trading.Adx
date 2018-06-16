namespace Rogozinski.Trading.Adx
{
    using System;
    using System.Linq;

    internal interface ITrueRangeCalculator
    {
        double Calculate(PricePoint previousPrice, PricePoint currentPrice);
    }

    internal class TrueRangeCalculator : ITrueRangeCalculator
    {
        public double Calculate(PricePoint previousPrice, PricePoint currentPrice)
        {
            var currentHighToLow = Math.Abs(currentPrice.High - currentPrice.Low);
            var currentHighToPreviousClose = Math.Abs(currentPrice.High - previousPrice.Close);
            var currentLowToPreviousClose = Math.Abs(currentPrice.Low - previousPrice.Close);

            return (new double[] { currentHighToLow, currentHighToPreviousClose, currentLowToPreviousClose }).Max();
        }
    }
}