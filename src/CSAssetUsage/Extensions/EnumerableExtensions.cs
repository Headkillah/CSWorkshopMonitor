using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    /// <summary>
    /// Represents a class containing extension methods for the Enumerable and IEnumerable types
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Performs an action on all elements of an enumerable list of items
        /// </summary>
        /// <typeparam name="T">The type of items in the list</typeparam>
        /// <param name="enumerable">The enumerable list of items</param>
        /// <param name="action">The action to perform on all items</param>
        public static void Do<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}
