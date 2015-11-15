using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WorkshopMonitor
{
    /// <summary>
    /// Represents a class holding information about a single building instance as collected by the building monitor
    /// </summary>
    public class OverwatchBuilding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OverwatchBuilding"/> class.
        /// </summary>
        /// <param name="buildingId">The building identifier</param>
        /// <param name="building">The building object</param>
        public OverwatchBuilding(ushort buildingId, Building building)
        {
            BuildingId = buildingId;
            SourcePackageId = ParsePackageId(building);
            TechincalName = building.Info.name;
        }

        /// <summary>
        /// Gets the building identifier
        /// </summary>
        public ushort BuildingId { get; private set; }

        /// <summary>
        /// Gets the name of the building.
        /// </summary>
        public string TechincalName { get; private set; }

        /// <summary>
        /// Gets the identifier of the package the building originated from
        /// </summary>
        public ulong SourcePackageId { get; private set; }

        private ulong ParsePackageId(Building building)
        {
            ulong result = 0;
            var match = Regex.Match(building.Info.name, @"^[\d]+");
            if (match.Success && !string.IsNullOrEmpty(match.Value)) 
                result = ulong.Parse(match.Value);
            return result;
        }
    }
}
