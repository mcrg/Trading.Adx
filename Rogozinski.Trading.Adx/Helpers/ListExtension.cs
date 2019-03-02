namespace Rogozinski.Trading.Adx
{
    using System.Collections.Generic;
    using System.Linq;
    using System;

    internal static class ListExtension
    {
        public static IList<T> Slice<T>(this IList<T> items, int startIndex, int? endIndex = null)
        {
            if (endIndex.HasValue)
            {
                return items.Skip(startIndex).Take(endIndex.Value - startIndex + 1).ToList();
            }
            return items.Skip(startIndex).ToList();
        }
    }
}