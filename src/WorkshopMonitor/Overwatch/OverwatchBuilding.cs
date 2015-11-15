using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WorkshopMonitor.Overwatch
{
    public class OverwatchBuilding
    {
        public OverwatchBuilding(ushort buildingId, Building building)
        {
            BuildingId = buildingId;
            SourcePackageId = ParsePackageId(building);
            TechincalName = building.Info.name;
        }

        public ushort BuildingId { get; private set; }

        public string TechincalName { get; private set; }

        public ulong SourcePackageId { get; private set; }

        private ulong ParsePackageId(Building building)
        {
            ulong result = 0;
            var match = Regex.Match(building.Info.name, RegexExpression.PackageId);
            if (match.Success && !string.IsNullOrEmpty(match.Value)) 
                result = ulong.Parse(match.Value);
            return result;
        }
    }
}
