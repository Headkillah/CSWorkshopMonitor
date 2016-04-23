using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Overwatch;

namespace WorkshopMonitor.UI
{
    public class CheckedChangedEventArgs : EventArgs
    {
        public CheckedChangedEventArgs(AssetType assetType, bool @checked)
        {
            AssetType = assetType;
            Checked = @checked;
        }

        public AssetType AssetType { get; private set; }

        public bool Checked { get; private set; }
    }
}
