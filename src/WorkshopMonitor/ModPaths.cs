using ColossalFramework.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    public static class ModPaths
    {
        private const string ModRootFolderName = "WorkshopMonitor";
        private const string ConfigurationFileName = "WorkshopMonitor.xml";
        private const string LogFileName = "WorkshopMonitor.log";

        public static string GetConfigurationFilePath()
        {
            return Path.Combine(GetModRootPath(), ConfigurationFileName);
        }

        public static string GetLogFilePath()
        {
            return Path.Combine(GetModRootPath(), LogFileName);
        }

        private static string GetModRootPath()
        {
            return Path.Combine(DataLocation.modsPath, ModRootFolderName);
        }
    }
}
