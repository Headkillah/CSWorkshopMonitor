using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Overwatch;

namespace WorkshopMonitor.Workshop
{
    public class WorkshopItem
    {
        public event EventHandler<EventArgs> InstanceCountUpdated;

        public WorkshopItem(ulong workshopId, string readableName, string technicalName, BuildingType buildingType)
        {
            WorkshopId = workshopId;
            ReadableName = readableName;
            TechnicalName = technicalName;
            BuildingType = buildingType;

            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public ulong WorkshopId { get; private set; }

        public string ReadableName { get; private set; }

        public string TechnicalName { get; private set; }

        public BuildingType BuildingType { get; private set; }

        public int InstanceCount { get; private set; }
        
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
