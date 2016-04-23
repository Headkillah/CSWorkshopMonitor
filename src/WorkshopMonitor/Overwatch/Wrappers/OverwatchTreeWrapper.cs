using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WorkshopMonitor.Overwatch
{
    public class OverwatchTreeWrapper : IOverwatchAssetWrapper
    {
        public OverwatchTreeWrapper(ushort treeInstanceId, TreeInstance treeInstance)
        {
            TreeId = treeInstanceId;
            SourcePackageId = ParsePackageId(treeInstance);
            TechnicalName = treeInstance.Info.name;
        }

        public ushort TreeId { get; private set; }

        public string TechnicalName { get; private set; }

        public ulong SourcePackageId { get; private set; }

        public ushort ParentAssetId { get; private set; }

        public int PropIndex { get; private set; }

        private ulong ParsePackageId(TreeInstance treeInstance)
        {
            ulong result = 0;
            var match = Regex.Match(treeInstance.Info.name, RegexExpression.PackageId);
            if (match.Success && !string.IsNullOrEmpty(match.Value)) 
                result = ulong.Parse(match.Value);
            return result;
        }
    }
}
