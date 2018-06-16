namespace Rogozinski.Trading.Adx
{
	using System.Collections.Generic;

	internal interface IDmPointCalculator
	{
		DmPointResult Calculate(IList<IPricePoint> pricePoints);
	}


    internal class DmPointResult
    {
        public double Tr14 { get; set; }
        public DmResult Dm14 { get; set; }
    }

	internal class DmPointCalculator : IDmPointCalculator
	{
        public DmPointResult Calculate(IList<IPricePoint> pricePoints)
		{
            /*
             * sum up all 14 TR1 to get TR14
             * sum up all 14 +DM1 to get +DM14
             * sum up all 14 -DM1 to get -DM14
             */
			throw new System.NotImplementedException();
		}
	}
}
