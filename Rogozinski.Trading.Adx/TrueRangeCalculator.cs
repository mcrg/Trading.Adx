namespace Rogozinski.Trading.Adx
{
    using System;
    using System.Linq;

    public class TrueRangeCalculator
    {
        public double Calculate(IPricePoint previousPrice, IPricePoint currentPrice)
        {
            var currentHighToLow = Math.Abs(currentPrice.High - currentPrice.Low);
            var currentHighToPreviousClose = Math.Abs(currentPrice.High - previousPrice.Close);
            var currentLowToPreviousClose = Math.Abs(currentPrice.Low - previousPrice.Close);

            return (new double[] { currentHighToLow, currentHighToPreviousClose, currentLowToPreviousClose }).Max();
        }
    }
}