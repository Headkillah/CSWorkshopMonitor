using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Overwatch;

namespace WorkshopMonitor.UI
{
    public class FilterChangedEventArgs : EventArgs
    {
        public FilterChangedEventArgs(AssetType newFilter)
        {
            NewFilter = newFilter;
        }

        public AssetType NewFilter { get; private set; }
    }
}
