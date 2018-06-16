using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Rogozinski.Trading.Adx.Tests")]
namespace Rogozinski.Trading.Adx
{
    using System;
    using System.Collections.Generic;

    internal interface IAdxPointCalculator
    {
        IEnumerable<AdxPointHistory> Calculate(AdxPointHistory previousAdxPoint, IList<PricePoint> pricePoints);
    }

    internal class AdxPointCalculator : IAdxPointCalculator
    {
        public IEnumerable<AdxPointHistory> Calculate(AdxPointHistory previousAdxPoint, IList<PricePoint> pricePoints)
        {
            foreach (var pricePoint in pricePoints)
            {
                /* for pricepoint at index i:
                 *  - calculate TR14*
                 *  - calculate +DM14*
                 *  - calculate -DM14*
                 *  - calculate +DI14
                 *  - calculate -DI14
                 *  - calculate DI Diff and DI Sum
                 *  - calculate DX
                 *  - 
                 */
            }
            throw new NotImplementedException();
        }
    }
}
