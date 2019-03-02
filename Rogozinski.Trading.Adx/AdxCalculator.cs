using System.Runtime.CompilerServices;
[assembly : InternalsVisibleTo("Rogozinski.Trading.Adx.Tests")]
[assembly : InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Rogozinski.Trading.Adx
{
    using System.Collections.Generic;
    using System;

    public interface IAdxCalculator
    {
        IEnumerable<AdxPoint> Calculate(IList<PricePoint> pricePoints);

        AdxPoint Calculate(AdxPoint prevAdx, PricePoint currentPrice);
    }

    internal class AdxCalculator : IAdxCalculator
    {
        const int Period = 14;
        private readonly IDmPointCalculator dmPointCalculator;
        private readonly IAdxPointCalculator adxPointCalculator;

        public AdxCalculator(IDmPointCalculator dmCalculator, IAdxPointCalculator adxCalculator)
        {
            this.dmPointCalculator = dmCalculator;
            this.adxPointCalculator = adxCalculator;
        }

        public IEnumerable<AdxPoint> Calculate(IList<PricePoint> pricePoints)
        {
            Check.EnoughElements(pricePoints, Period * 2 + 1);

            var dmItem = dmPointCalculator.Calculate(pricePoints.Slice(0, Period - 1));
            var adxPoint = adxPointCalculator.Calculate(dmItem, pricePoints.Slice(Period - 1, Period * 2 - 1));

            var restPricePoints = pricePoints.Slice(Period * 2);

            var adxPoints = new List<AdxPoint>();
            adxPoints.Add(adxPoint);
            foreach (var price in restPricePoints)
            {
                adxPoint = adxPointCalculator.Calculate(adxPoint, price);
                adxPoints.Add(adxPoint);
            }

            return adxPoints;
        }

        public AdxPoint Calculate(AdxPoint prevAdx, PricePoint currentPrice)
        {
            return adxPointCalculator.Calculate(prevAdx, currentPrice);
        }
    }
}