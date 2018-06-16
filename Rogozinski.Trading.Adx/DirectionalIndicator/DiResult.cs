namespace Rogozinski.Trading.Adx
{
    public class DiResult
    {
        public double PlusDi { get; }

        public double MinusDi { get; }

		public DiResult(double plusDi, double minusDi)
        {
            PlusDi = plusDi;
            MinusDi = minusDi;
        }
    }
}
