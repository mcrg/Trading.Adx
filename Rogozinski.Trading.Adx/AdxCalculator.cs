using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Rogozinski.Trading.Adx.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Rogozinski.Trading.Adx
{
    using System;
    using System.Collections.Generic;

    public interface IAdxCalculator
    {
        IEnumerable<AdxPoint> Calculate(IList<PricePoint> pricePoints);

        AdxPoint Calculate(AdxPointHistory previousAdxPointHistory, PricePoint currentPricePoint);
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

        public IEnumerable<AdxPoint> Calculate(IList<PricePoint> pricePoints)
        {
            Check.EnoughElements(pricePoints, Period * 2 + 1);
            
            var dmItem = dmPointCalculator.Calculate(pricePoints.Slice(Period - 1));
            var dxItem = dxPointCalculator.Calculate(dmItem, pricePoints.Slice(Period, Period * 2 - 1));

            var restPricePoints = pricePoints.Slice(Period * 2);
            var adxPoints = adxPointCalculator.Calculate(dxItem, restPricePoints);

            return new List<AdxPoint>();
        }

        public AdxPoint Calculate(AdxPointHistory previousAdxPointHistory, PricePoint currentPricePoint)
        {
            throw new NotImplementedException();
        }
    }
}
