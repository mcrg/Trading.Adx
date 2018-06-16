namespace Rogozinski.Trading.Adx
{
    using System.Collections.Generic;

    internal interface IDxPointCalculator
    {
        AdxPointHistory Calculate(DmPointResult dmResult, IList<PricePoint> pricePoints);
    }
}
