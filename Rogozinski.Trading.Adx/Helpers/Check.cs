namespace Rogozinski.Trading.Adx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Check
    {
        public static void EnoughElements<T>(IList<T> collection, int minSize)
        {
            if (collection.Count < minSize)
            {
                throw new ArgumentException($"Collection should have at least {minSize} element(s)!");
            }
        }
    }
}