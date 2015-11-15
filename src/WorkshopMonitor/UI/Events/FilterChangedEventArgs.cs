using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Overwatch;

namespace WorkshopMonitor.UI
{
    public class FilterChangedEventArgs : EventArgs
    {
        public FilterChangedEventArgs(BuildingType newFilter)
        {
            NewFilter = newFilter;
        }

        public BuildingType NewFilter { get; private set; }
    }
}
