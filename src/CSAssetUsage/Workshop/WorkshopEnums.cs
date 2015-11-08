using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    /// <summary>
    /// Defines constants identifying the type of field to sort the list of workshop items on
    /// </summary>
    public enum SortableWorkshopItemField
    {
        /// <summary>
        /// The default invalid value
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// Orders by the item type
        /// </summary>
        ItemType,
        /// <summary>
        /// Orders by the building instance count
        /// </summary>
        InstanceCount,
        /// <summary>
        /// Orders by the item name
        /// </summary>
        Name
    }
}
