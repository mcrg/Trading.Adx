namespace Rogozinski.Trading.Adx
{
    public class AdxCalculatorFactory
    {
        public IAdxCalculator Create()
        {
            var trueRangeCalculator = new TrueRangeCalculator();
            var dmCalculator = new DmCalculator();
            return new AdxCalculator(
                new DmPointCalculator(trueRangeCalculator, dmCalculator),
                new AdxPointCalculator(trueRangeCalculator, new AccumulationCalculator(), dmCalculator));
        }
    }
}