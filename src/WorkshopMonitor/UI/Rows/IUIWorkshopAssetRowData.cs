using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorkshopMonitor.Overwatch;
using WorkshopMonitor.Workshop;

namespace WorkshopMonitor.UI
{
    public interface IUIWorkshopAssetRowData
    {
        event EventHandler<EventArgs> InstanceCountUpdated;

        ulong WorkshopId { get; }
        string ReadableName { get; }
        Guid WorkshopAssetId { get; }
        int InstanceCount { get; }
        Color32 Color { get; }
        string SpriteName { get; }
        AssetType AssetType { get; }
    }
}
