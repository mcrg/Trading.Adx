namespace Rogozinski.Trading.Adx
{
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
