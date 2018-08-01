namespace Rogozinski.Trading.Adx
{
    public class DmPointResult
    {
        public double Tr14 { get; }
        public DmResult Dm14 { get; }

        public DmPointResult(double tr14, double plusDm14, double minusDm14)
        {
            Tr14 = tr14;
            Dm14 = new DmResult(plusDm14, minusDm14);
        }
    }
}
