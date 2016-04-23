using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WorkshopMonitor.Overwatch
{
    public class OverwatchBuildingWrapper : IOverwatchAssetWrapper
    {
        public OverwatchBuildingWrapper(ushort buildingId, Building building)
        {
            BuildingId = buildingId;
            SourcePackageId = ParsePackageId(building);
            TechnicalName = building.Info.name;
        }

        public ushort BuildingId { get; private set; }

        public string TechnicalName { get; private set; }

        public ulong SourcePackageId { get; private set; }

        public ushort ParentAssetId
        {
            get { return 0; }
        }

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
