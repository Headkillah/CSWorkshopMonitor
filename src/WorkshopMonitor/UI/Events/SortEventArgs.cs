using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Workshop;

namespace WorkshopMonitor.UI
{
    public class SortEventArgs : EventArgs
    {
        public SortEventArgs(SortableWorkshopAssetField sortField)
        {
            SortField = sortField;
        }

        public SortableWorkshopAssetField SortField { get; private set; }
    }
}
