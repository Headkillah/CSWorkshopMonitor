using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    /// <summary>
    /// Represents an eventargs class for the FilterChanged event
    /// </summary>
    public class FilterChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterChangedEventArgs"/> class.
        /// </summary>
        /// <param name="newFilter">The new filter.</param>
        public FilterChangedEventArgs(BuildingType newFilter)
        {
            NewFilter = newFilter;
        }

        /// <summary>
        /// Gets the new filter.
        /// </summary>
        public BuildingType NewFilter { get; private set; }
    }
}
