using ColossalFramework.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    /// <summary>
    /// Represents a class responsible for generating full paths for logging, configuration loading/saving, etc.
    /// </summary>
    public static class ModPaths
    {
        private const string ModRootFolderName = "WorkshopMonitor";
        private const string ConfigurationFileName = "WorkshopMonitor.xml";
        private const string LogFileName = "WorkshopMonitor.log";

        /// <summary>
        /// Gets the full path to the mod configuration file
        /// </summary>
        public static string GetConfigurationFilePath()
        {
            return Path.Combine(GetModRootPath(), ConfigurationFileName);
        }

        /// <summary>
        /// Gets the full path to the mod log file
        /// </summary>
        public static string GetLogFilePath()
        {
            return Path.Combine(GetModRootPath(), LogFileName);
        }

        /// <summary>
        /// Gets the full path to the mod root folder
        /// </summary>
        private static string GetModRootPath()
        {
            return Path.Combine(DataLocation.modsPath, ModRootFolderName);
        }
    }
}
