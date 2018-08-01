namespace Rogozinski.Trading.Adx
{
    internal interface IAccumulationCalculator
    {
        double GetCurrentAccumulatedValue(double previousAccumulatedValue, double currentValue);
    }

    internal class AccumulationCalculator : IAccumulationCalculator
    {
        public double GetCurrentAccumulatedValue(double previousAccumulatedValue, double currentValue)
        {
            return System.Math.Round(previousAccumulatedValue - (previousAccumulatedValue / 14)+ currentValue, 2);
        }
    }
}