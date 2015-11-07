using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    /// <summary>
    /// Represents an eventargs class for the CheckedChanged event
    /// </summary>
    public class CheckedChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedChangedEventArgs"/> class.
        /// </summary>
        /// <param name="buildingType">Type of the building.</param>
        /// <param name="checked">if set to <c>true</c> [checked].</param>
        public CheckedChangedEventArgs(BuildingType buildingType, bool @checked)
        {
            BuildingType = buildingType;
            Checked = @checked;
        }

        /// <summary>
        /// Gets the type of the building.
        /// </summary>
        /// <value>
        /// The type of the building.
        /// </value>
        public BuildingType BuildingType { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this the item was checked
        /// </summary>
        public bool Checked { get; private set; }
    }
}
