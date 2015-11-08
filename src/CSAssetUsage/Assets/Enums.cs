using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
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
        /// Orders by the asset type
        /// </summary>
        AssetType,
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
