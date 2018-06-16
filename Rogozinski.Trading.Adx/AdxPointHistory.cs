using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Rogozinski.Trading.Adx.Tests")]
namespace Rogozinski.Trading.Adx
{
    public class AdxPointHistory
    {
        public double Tr14 { get; set; }
        public DmResult Dm14 { get; set; }
        public DiResult Di14 { get; set; }
        public double Adx { get; set; }
    }
}
