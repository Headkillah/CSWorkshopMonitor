using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WorkshopMonitor.Overwatch
{
    public class OverwatchPropWrapper : IOverwatchAssetWrapper
    {
        public OverwatchPropWrapper(ushort propInstanceId, PropInstance propInstance)
        {
            PropId = propInstanceId;
            SourcePackageId = ParsePackageId(propInstance.Info);
            TechnicalName = propInstance.Info.name;
        }

        public OverwatchPropWrapper(ushort parentAssetId, int propIndex, PropInfo propInfo)
        {
            ParentAssetId = parentAssetId;
            PropIndex = propIndex;
            SourcePackageId = ParsePackageId(propInfo);
            TechnicalName = propInfo.name;
        }

        public ushort PropId { get; private set; }

        public string TechnicalName { get; private set; }

        public ulong SourcePackageId { get; private set; }

        public ushort ParentAssetId { get; private set; }

        public int PropIndex { get; private set; }

        private ulong ParsePackageId(PropInfo propInfo)
        {
            ulong result = 0;
            var match = Regex.Match(propInfo.name, RegexExpression.PackageId);
            if (match.Success && !string.IsNullOrEmpty(match.Value)) 
                result = ulong.Parse(match.Value);
            return result;
        }
    }
}
