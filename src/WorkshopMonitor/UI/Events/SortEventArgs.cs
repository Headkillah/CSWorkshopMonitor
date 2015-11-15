using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Workshop;

namespace WorkshopMonitor.UI
{
    public class SortEventArgs : EventArgs
    {
        public SortEventArgs(SortableWorkshopItemField sortField)
        {
            SortField = sortField;
        }

        public SortableWorkshopItemField SortField { get; private set; }
    }
}
