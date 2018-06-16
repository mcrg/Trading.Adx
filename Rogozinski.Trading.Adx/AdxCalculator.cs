using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Rogozinski.Trading.Adx.Tests")]
namespace Rogozinski.Trading.Adx
{
    using System;
    using System.Collections.Generic;

    public interface IAdxCalculator
    {
        IEnumerable<AdxPoint> Calculate(IList<IPricePoint> pricePoints);

        AdxPoint Calculate(AdxPointHistory previousAdxPointHistory, IPricePoint currentPricePoint);
    }

    internal class AdxCalculator : IAdxCalculator
    {
        const int Period = 14;
        private readonly IDmPointCalculator dmPointCalculator;
        private readonly IDxPointCalculator dxPointCalculator;
        private readonly IAdxPointCalculator adxPointCalculator;

        public AdxCalculator(IDmPointCalculator dmCalculator, IDxPointCalculator dxCalculator, IAdxPointCalculator adxCalculator)
        {
            this.dmPointCalculator = dmCalculator;
            this.dxPointCalculator = dxCalculator;
            this.adxPointCalculator = adxCalculator;
        }

        public IEnumerable<AdxPoint> Calculate(IList<IPricePoint> pricePoints)
        {
            if (pricePoints.Count <= Period * 2)
            {
                throw new ArgumentException($"Not enough price points, requires at least {Period * 2 + 1}");
            }


            var dmItem = dmPointCalculator.Calculate(pricePoints.Slice(Period - 1));
            var dxItem = dxPointCalculator.Calculate(dmItem, pricePoints.Slice(Period, Period * 2 - 1));

            var restPricePoints = pricePoints.Slice(Period * 2);
            var adxPoints = adxPointCalculator.Calculate(dxItem, restPricePoints);

            return new List<AdxPoint>();
        }

        public AdxPoint Calculate(AdxPointHistory previousAdxPointHistory, IPricePoint currentPricePoint)
        {
            throw new NotImplementedException();
        }
    }
}
