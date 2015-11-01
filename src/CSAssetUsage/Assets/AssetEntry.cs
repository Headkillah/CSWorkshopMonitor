using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    /// <summary>
    /// Represents a class holding the data of a single asset used for showing asset information in an assetrow
    /// </summary>
    public class AssetEntry
    {
        /// <summary>
        /// Occurs when the instance count of the entry has been updated
        /// </summary>
        public event EventHandler<EventArgs> InstanceCountUpdated;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetEntry"/> class.
        /// </summary>
        /// <param name="packageId">The asset package identifier</param>
        /// <param name="metadata">The asset metadata</param>
        public AssetEntry(string packageId, CustomAssetMetaData metadata)
        {
            PackageId = packageId;
            Metadata = metadata;
        }

        /// <summary>
        /// Gets the asset package identifier.
        /// </summary>
        public string PackageId { get; private set; }

        /// <summary>
        /// Gets the asset metadata
        /// </summary>
        public CustomAssetMetaData Metadata { get; private set; }

        /// <summary>
        /// Gets the number of building instances that are created from the asset
        /// </summary>
        public int InstanceCount { get; private set; }

        /// <summary>
        /// Gets the number of building instances that are created from the asset
        /// </summary>
        /// <param name="instanceCount">The number of building instances</param>
        public void SetInstanceCount(int instanceCount)
        {
            InstanceCount = instanceCount;
            OnInstanceCountUpdated();
        }

        /// <summary>
        /// Raises the InstanceCountUpdated event
        /// </summary>
        protected virtual void OnInstanceCountUpdated()
        {
            var handler = this.InstanceCountUpdated;
            if (handler != null)
                handler.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Defines constants identifying the type of field to sort the list of assets on
    /// </summary>
    public enum SortableAssetEntryField
    {
        /// <summary>
        /// The default invalid value
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// Orders by the building instance count
        /// </summary>
        InstanceCount,
        /// <summary>
        /// Orders by the asset name
        /// </summary>
        Name
    }
}
