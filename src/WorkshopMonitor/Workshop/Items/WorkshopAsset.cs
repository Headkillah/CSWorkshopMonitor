using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorkshopMonitor.Overwatch;
using WorkshopMonitor.UI;

namespace WorkshopMonitor.Workshop
{
    public abstract class WorkshopAsset : IUIWorkshopAssetRowData
    {
        public event EventHandler<EventArgs> InstanceCountUpdated;

        public abstract AssetType AssetType { get; }

        internal abstract void UpdateInstanceCount();

        public WorkshopAsset(ulong workshopId, string readableName, string technicalName)
        {
            WorkshopId = workshopId;
            ReadableName = readableName;
            TechnicalName = technicalName;

            WorkshopAssetId = Guid.NewGuid();
        }

        public Guid WorkshopAssetId { get; private set; }

        public ulong WorkshopId { get; private set; }

        public string ReadableName { get; private set; }

        public string TechnicalName { get; private set; }

        public int InstanceCount { get; private set; }

        public abstract Color32 Color { get; }

        public abstract string SpriteName { get; }

        public void SetInstanceCount(int instanceCount)
        {
            InstanceCount = instanceCount;
            OnInstanceCountUpdated();
        }

        protected virtual void OnInstanceCountUpdated()
        {
            var handler = this.InstanceCountUpdated;
            if (handler != null)
                handler.Invoke(this, EventArgs.Empty);
        }
    }
}
