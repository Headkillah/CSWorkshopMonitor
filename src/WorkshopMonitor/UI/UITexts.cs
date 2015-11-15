using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.UI
{
    public static class UITexts
    {
        public static string ModName
        {
            get { return "Workshop Monitor"; }
        }

        public static string ModDescription
        {
            get { return "Displays an overview of workshop item usage in a game"; }
        }

        public static string PackageIdColumnLabel
        {
            get { return "Id"; }
        }

        public static string WorkshopItemTypeColumnLabel
        {
            get { return "Type"; }
        }

        public static string WorkshopItemNameColumnLabel
        {
            get { return "Workshop Name"; }
        }

        public static string NumberUsedColumnLabel
        {
            get { return "# Used"; }
        }

        public static string WindowTitle
        {
            get { return "Workshop Monitor"; }
        }

        public static string WorkshopItemInfoButtonToolTip
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

        public static string WorkshopItemInfoOpenInBrowserTitle
        {
            get { return "Open in browser"; }
        }

        public static string WorkshopItemInfoOpenInBrowserMessage
        {
            get { return "Could not open the workshop item in the in-game workshop. Do you want to open the workshop item in a browser?"; }
        }

        public static string FilterButtonSelectAllText
        {
            get { return "all"; }
        }

        public static string FilterButtonSelectNoneText
        {
            get { return "none"; }
        }
    }
}
