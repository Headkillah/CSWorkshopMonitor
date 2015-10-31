using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    public class AssetEntry
    {
        public event EventHandler<EventArgs> InstanceCountUpdated;

        public AssetEntry(string packageId, CustomAssetMetaData metadata)
        {
            PackageId = packageId;
            Metadata = metadata;
        }
        
        public string PackageId { get; private set; }

        public CustomAssetMetaData Metadata { get; private set; }

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

    public enum SortableAssetEntryField
    {
        Invalid = 0,
        InstanceCount,
        Name
    }
}
