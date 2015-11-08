using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    public static class UITexts
    {
        public static string ModName
        {
            get { return "Asset Usage"; }
        }

        public static string ModDescription
        {
            get { return "Displays the usage of custom assets in a game"; }
        }

        public static string PackageIdColumnLabel
        {
            get { return "Id"; }
        }

        public static string AssetTypeColumnLabel
        {
            get { return "Type"; }
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
            get { return "Open in workshop"; }
        }

        public static string ModSettingsGroupLabel
        {
            get { return "Mod settings"; }
        }

        public static string ModSettingsDebugLoggingOption
        {
            get { return "Enable debug logging (for advanced usage only)"; }
        }

        public static string AssetInfoOpenInBrowserTitle
        {
            get { return "Open in browser"; }
        }

        public static string AssetInfoOpenInBrowserMessage
        {
            get { return "Could not open the asset in the in-game workshop. Do you want to open the asset in a browser?"; }
        }
    }
}
