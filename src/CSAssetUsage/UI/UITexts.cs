using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    public static class UITexts
    {
        public static string PackageIdColumnLabel
        {
            get { return "Id"; }
        }
        
        public static string AssetNameColumnLabel
        {
            get { return "Asset Name"; }
        }

        public static string NumberUsedColumnLabel
        {
            get { return "# Used"; }
        }

        public static string WindowTitle
        {
            get { return "Asset Usage"; }
        }

        public static string AssetInfoButtonToolTip
        {
            get { return "Open in workshop (Note! This will open the steam workshop in your default browser!)"; }
        }

        public static string ModSettingsGroupLabel
        {
            get { return "Mod settings"; }
        }

        public static string ModSettingsDebugLoggingOption
        {
            get { return "Enable debug logging (for advanced usage only)"; }
        }
    }
}
