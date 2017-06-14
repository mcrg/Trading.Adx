namespace Rogozinski.Trading.Adx
{
    public class PricePoint : IPricePoint
    {
        public double Open { get; set; }

        public double Close { get; set; }

        public double High { get; set; }

        public double Low { get; set; }
    }

    public interface IPricePoint
    {
        double Open { get; }

        double Close { get; }

        double High { get; }

        double Low { get; }
    }
}
