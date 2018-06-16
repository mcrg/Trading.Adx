namespace Rogozinski.Trading.Adx
{
    internal interface IDmCalculator
    {
        DmResult Calculate(PricePoint previousPrice, PricePoint currentPrice);
    }

    internal class DmCalculator : IDmCalculator
    {
        public DmResult Calculate(PricePoint previousPrice, PricePoint currentPrice)
        {
            var highDiff = currentPrice.High - previousPrice.High;
            var lowDiff = previousPrice.Low - currentPrice.Low;

            if (highDiff <= 0 && lowDiff <= 0)
            {
                return new DmResult(0, 0);
            }

            if (highDiff > lowDiff)
            {
                return new DmResult(highDiff, 0);
            }

            return new DmResult(0, lowDiff);
        }
    }
}