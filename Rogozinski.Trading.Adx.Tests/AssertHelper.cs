using Xunit;

namespace Rogozinski.Trading.Adx.Tests
{
    public static class AssertHelper
    {

        public static void AdxPointsEqual(AdxPoint expected, AdxPoint actual)
        {
            if (expected.Price != null)
            {
                Assert.Same(expected.Price, actual.Price);
            }
            Assert.Equal(expected.Adx, actual.Adx);
            Assert.Equal(expected.Tr14, actual.Tr14);
            Assert.Equal(expected.Dm14.PlusDm, actual.Dm14.PlusDm);
            Assert.Equal(expected.Dm14.MinusDm, actual.Dm14.MinusDm);
            Assert.Equal(expected.Di14.PlusDi, actual.Di14.PlusDi);
            Assert.Equal(expected.Di14.MinusDi, actual.Di14.MinusDi);
        }
    }
}