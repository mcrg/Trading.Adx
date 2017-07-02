namespace Rogozinski.Trading.Adx
{
    public interface IDmCalculator
    {
        DmResult Calculate(IPricePoint previousPrice, IPricePoint currentPrice);
    }

    public class DmCalculator : IDmCalculator
    {
        public DmResult Calculate(IPricePoint previousPrice, IPricePoint currentPrice)
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

    public class DmResult
    {
        public double PlusDm { get; }

        public double MinusDm { get; }

        public DmResult(double plusDm, double minusDm)
        {
            PlusDm = plusDm;
            MinusDm = minusDm;
        }
    }
}
