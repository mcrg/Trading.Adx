namespace Rogozinski.Trading.Adx
{
    public class PricePoint : IPricePoint
    {
        public PricePoint() { }

        public PricePoint(double open, double high, double low, double close)
        {
            Open = open;
            High = high;
            Low = low;
            Close = close;
        }

        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Close { get; set; }
    }

    public interface IPricePoint
    {
        double Open { get; }

        double High { get; }

        double Low { get; }

        double Close { get; }
    }
}
