using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CSAssetUsage
{
    /// <summary>
    /// Represents a class holding the configuration options for the AssetUsage mod
    /// </summary>
    public class Configuration
    {
        private string _filename;

        /// <summary>
        /// Gets or sets a flag indicating whether debug logging is enabled
        /// </summary>
        public bool DebugLogging { get; set; }

        /// <summary>
        /// Loads the configuration file specified by the filename parameter and returns the configuration object.
        /// </summary>
        /// <param name="filename">The configuration filename</param>
        /// <returns>A configuration object loaded from the configuration file, or an empty configuration object if the specified filename was not found</returns>
        public static Configuration LoadConfig(string filename)
        {
            ModLogger.Debug("Loading configuration from '{0}'", filename);

            Configuration result = null;
            
            // Check if the file exists. If so, deserialize it to a new instance. If not so, create a new empty instance
            if (File.Exists(filename))
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    result = (Configuration)new XmlSerializer(typeof(Configuration)).Deserialize(sr);
                }
            }
            else
                result = new Configuration();

            // Assign the filename to the result. This is used later on when saving the configuration.
            result._filename = filename;

            ModLogger.Debug("Configuration loaded from '{0}'", filename);

            return result;
        }

        /// <summary>
        /// Applies all configuration options of the configuration object to the mod
        /// </summary>
        public void ApplyConfig()
        {
            ModLogger.Debug("Applying configuration options");
            ModLogger.DebugLogging = this.DebugLogging;
        }

        /// <summary>
        /// Saves the configuration to file. The filename specified when the configuration was loaded is used for saving.
        /// </summary>
        public void SaveConfig()
        {
            ModLogger.Debug("Saving configuration to '{0}'", _filename);

            // Make sure the directory exists
            string dirname = Path.GetDirectoryName(_filename);
            if (!string.IsNullOrEmpty(dirname) && !Directory.Exists(dirname))
                Directory.CreateDirectory(dirname);

            // Serialize the configuration to the xml file
            using (StreamWriter sw = new StreamWriter(_filename))
            {
                new XmlSerializer(this.GetType()).Serialize(sw, this);
            }
        }
    }
}
