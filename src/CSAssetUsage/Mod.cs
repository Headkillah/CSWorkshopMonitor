using ColossalFramework.IO;
using ICities;
using System;
using System.IO;

namespace CSAssetUsage
{
    /// <summary>
    /// Represents the main entrypoint of the mod that is loaded by CS
    /// </summary>
    public class Mod : IUserMod, IUserModStateChange, IUserModConfiguration
    {
        private const string ModName = "Asset Usage";
        private const string ModDescription = "Displays the usage of custom assets in a game";
        private string _settingsFilePath;
        private Configuration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mod"/> class.
        /// </summary>
        public Mod()
        {
            ModLogger.ModLoaded();
        }

        /// <summary>
        /// Gets the name of the mod as displayed in the CS mod configuration window
        /// </summary>
        public string Name
        {
            get { return ModName; }
        }

        /// <summary>
        /// Gets the description of the mod as displayed in the CS mod configuration window
        /// </summary>
        public string Description
        {
            get { return ModDescription; }
        }

        /// <summary>
        /// Loads the configuration when the mod is enabled
        /// </summary>
        public void OnEnabled()
        {
            _settingsFilePath = ModPaths.GetConfigurationFilePath();
            Load();
            ModLogger.Debug("AssetUsage mod enabled");
        }

        /// <summary>
        /// Unloads the configuration when the mod is disabled
        /// </summary>
        public void OnDisabled()
        {
            ModLogger.Debug("AssetUsage mod disabled");
            this.Unload();
        }

        /// <summary>
        /// Creates the custom configuration UI for the mod. Called when a mod is loaded by CS.
        /// </summary>
        /// <param name="helper">A reference to the CS helper class for creating configuration UI components</param>
        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelper uiHelper = helper as UIHelper;
            if (uiHelper != null)
            {
                var optionsPanel = new UIModOptionsPanelBuilder(uiHelper, _configuration);
                optionsPanel.CreateUI();
                ModLogger.Debug("Options panel created");
            }
            else
            {
                ModLogger.Warning("Could not populate the settings panel, helper is null or not a UIHelper");
            }
        }

        /// <summary>
        /// Loads the configuration from the fixed AssetUsage configuration file
        /// </summary>
        private void Load()
        {
            try
            {

                _configuration = Configuration.LoadConfig(_settingsFilePath);
            }
            catch (Exception ex)
            {
                ModLogger.Warning("An error occured while loading mod configuration from file '{0}', the default configuration will be applied", _settingsFilePath);
                ModLogger.Exception(ex);

                // Always create a configuration object, even when the file could not be loaded. This way the mod will not crash when the file could not be loaded
                _configuration = new Configuration();
            }

            // Apply the configuration to the running mod
            _configuration.ApplyConfig();
        }

        /// <summary>
        /// Unloads the configuration by saving it to the fixed AssetUsage configuration file
        /// </summary>
        private void Unload()
        {
            try
            {
                _configuration.SaveConfig();
            }
            catch (Exception ex)
            {
                ModLogger.Warning("An error occured while saving mod configuration to file '{0}', mod configuration is not saved", _settingsFilePath);
                ModLogger.Exception(ex);
            }
        }
    }
}
