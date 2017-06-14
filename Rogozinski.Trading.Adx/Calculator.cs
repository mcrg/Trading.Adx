namespace Rogozinski.Trading.Adx
{
    using System;
    using System.Collections.Generic;

    public interface ICalculator
    {
        IEnumerable<AdxPoint> Calculate(IEnumerable<IPricePoint> pricePoints);
    }

    public class Calculator : ICalculator
    {
        public IEnumerable<AdxPoint> Calculate(IEnumerable<IPricePoint> pricePoints) => throw new NotImplementedException();
    }
}
