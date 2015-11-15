using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    public static class RegexExpression
    {
        public const string PackageId = @"^[\d]+";
        public const string BuildingName = @"^(?<packageid>[\d]+)\.(?<prefabname>.*)_data";
    }
}
