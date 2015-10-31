using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    public class OverwatchBuilding
    {
        public OverwatchBuilding(ushort buildingId, Building building, BuildingType type)
        {
            BuildingId = buildingId;
            Building = building;
            Type = type;
        }

        public ushort BuildingId { get; private set; }

        public Building Building { get; private set; }

        public BuildingType Type { get; private set; }
    }
}
