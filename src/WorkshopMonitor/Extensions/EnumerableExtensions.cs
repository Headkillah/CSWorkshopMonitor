using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    public static class EnumerableExtensions
    {
        public static void DoAll<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}
