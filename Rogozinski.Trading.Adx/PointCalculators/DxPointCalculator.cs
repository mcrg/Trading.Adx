namespace Rogozinski.Trading.Adx
{
    using System.Collections.Generic;
    using System;

    internal interface IDxPointCalculator
    {
        AdxPointHistory Calculate(DmPointResult dmResult, IList<PricePoint> pricePoints);
    }

    internal class DxPointCalculator : IDxPointCalculator
    {
        private readonly ITrueRangeCalculator trCalculator;
        private readonly IAccumulationCalculator accCalculator;
        private readonly IDmCalculator dmCalculator;

        public DxPointCalculator(ITrueRangeCalculator trCalculator, IAccumulationCalculator accCalculator, IDmCalculator dmCalculator)
        {
            this.trCalculator = trCalculator;
            this.accCalculator = accCalculator;
            this.dmCalculator = dmCalculator;
        }
        
        public AdxPointHistory Calculate(DmPointResult dmResult, IList<PricePoint> pricePoints)
        {
            Check.EnoughElements(pricePoints, 1);
            var tr14 = dmResult.Tr14;
            var minusDm14 = dmResult.Dm14.MinusDm;
            var plusDm14 = dmResult.Dm14.PlusDm;
            double dx = 0;
            double plusDi14 = 0;
            double minusDi14 = 0;
            for (int i = 1; i < pricePoints.Count; i++)
            {
                var prevPrice = pricePoints[i - 1];
                var currentPrice = pricePoints[i];
                var tr = trCalculator.Calculate(prevPrice, currentPrice);
                tr14 = accCalculator.GetCurrentAccumulatedValue(tr14, tr);
                var dm = dmCalculator.Calculate(prevPrice, currentPrice);
                minusDm14 = accCalculator.GetCurrentAccumulatedValue(minusDm14, dm.MinusDm);
                plusDm14 = accCalculator.GetCurrentAccumulatedValue(plusDm14, dm.PlusDm);
                plusDi14 = Math.Round(plusDm14 / tr14 * 100, 0);
                minusDi14 = Math.Round(minusDm14 / tr14 * 100, 0);
                double diDiff = Math.Abs(plusDi14 - minusDi14);
                double diSum = plusDi14 + minusDi14;
                dx += Math.Round(diDiff / diSum * 100, 0);
            }

            return new AdxPointHistory()
            {
                Tr14 = tr14,
                Dm14 = new DmResult(plusDm14, minusDm14),
                Di14 = new DiResult(plusDi14, minusDi14),
                Adx = Math.Round(dx / 14, 0)
            };
        }
    }
}