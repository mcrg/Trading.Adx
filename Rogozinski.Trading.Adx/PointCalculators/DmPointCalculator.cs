namespace Rogozinski.Trading.Adx
{
    using System.Collections.Generic;

    internal interface IDmPointCalculator
    {
        DmPointResult Calculate(IList<PricePoint> pricePoints);
    }

    internal class DmPointCalculator : IDmPointCalculator
    {
        private readonly ITrueRangeCalculator trueRangeCalculator;
        private readonly IDmCalculator dmCalculator;

        public DmPointCalculator(ITrueRangeCalculator trueRangeCalculator, IDmCalculator dmCalculator)
        {
            this.trueRangeCalculator = trueRangeCalculator;
            this.dmCalculator = dmCalculator;
        }

        public DmPointResult Calculate(IList<PricePoint> pricePoints)
        {
            Check.EnoughElements(pricePoints, 2);

            double trueRangeSum = 0;
            double plusDmSum = 0;
            double minusDmSum = 0;
            for (int i = 1; i < pricePoints.Count; i++)
            {
                var previousPrice = pricePoints[i - 1];
                var currentPrice = pricePoints[i];
                trueRangeSum += trueRangeCalculator.Calculate(previousPrice, currentPrice);
                var dm = dmCalculator.Calculate(previousPrice, currentPrice);
                plusDmSum += dm.PlusDm;
                minusDmSum += dm.MinusDm;
            }

            return new DmPointResult(trueRangeSum, plusDmSum, minusDmSum);
        }
    }
}