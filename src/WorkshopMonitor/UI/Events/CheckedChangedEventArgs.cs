using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Overwatch;

namespace WorkshopMonitor.UI
{
    public class CheckedChangedEventArgs : EventArgs
    {
        public CheckedChangedEventArgs(BuildingType buildingType, bool @checked)
        {
            BuildingType = buildingType;
            Checked = @checked;
        }

        public BuildingType BuildingType { get; private set; }

        public bool Checked { get; private set; }
    }
}
