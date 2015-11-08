using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    /// <summary>
    /// Represents an eventargs class for the Sort event
    /// </summary>
    public class SortEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortEventArgs"/> class.
        /// </summary>
        /// <param name="sortField">The sort field.</param>
        public SortEventArgs(SortableWorkshopItemField sortField)
        {
            SortField = sortField;
        }

        /// <summary>
        /// Gets the sort field.
        /// </summary>
        public SortableWorkshopItemField SortField { get; private set; }
    }
}
