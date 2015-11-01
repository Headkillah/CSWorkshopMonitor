﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
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
        /// <param name="type">The type of the building</param>
        public OverwatchBuilding(ushort buildingId, Building building, BuildingType type)
        {
            BuildingId = buildingId;
            Building = building;
            Type = type;
        }

        /// <summary>
        /// Gets the building identifier
        /// </summary>
        public ushort BuildingId { get; private set; }

        /// <summary>
        /// Gets the building object
        /// </summary>
        public Building Building { get; private set; }

        /// <summary>
        /// Gets the type of the building
        /// </summary>
        public BuildingType Type { get; private set; }
    }
}
