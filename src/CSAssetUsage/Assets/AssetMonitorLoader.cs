using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    /// <summary>
    /// Represents a class responsible for starting/stopping the asset monitor when a game is started or exited
    /// </summary>
    public class AssetMonitorLoader : LoadingExtensionBase
    {
        private LoadMode _mode;

        /// <summary>
        /// Called when the asset monitor loader is created
        /// </summary>
        /// <param name="loading">The loading instance</param>
        public override void OnCreated(ILoading loading)
        {
            ModLogger.Debug("AssetMonitorLoader created");
        }

        /// <summary>
        /// Called when the asset monitor loader is release
        /// </summary>
        public override void OnReleased()
        {
            ModLogger.Debug("AssetMonitorLoader Released");
        }

        /// <summary>
        /// Called when a new/existing game has been loaded by the user, starts the asset monitor
        /// </summary>
        /// <param name="mode">The mode</param>
        public override void OnLevelLoaded(LoadMode mode)
        {
            // Store the mode (used when unloading again)
            _mode = mode;
            
            // Don't start in asset and map editor
            if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
                return;

            try
            {
                // Start the asset monitor
                ModLogger.Debug("Starting asset monitor");
                AssetMonitor.Instance.Start();
                ModLogger.Debug("Asset monitor started");
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while starting the asset monitor");
                ModLogger.Exception(ex);
            }
        }

        /// <summary>
        /// Called when the user exits the game, stops the asset monitor
        /// </summary>
        public override void OnLevelUnloading()
        {
            // Don't stop in asset and map editor
            if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
                return;

            try
            {
                // Stop the asset monitor
                ModLogger.Debug("Stopping asset monitor");
                AssetMonitor.Instance.Stop();
                ModLogger.Debug("Asset monitor stopped");
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while stopping the asset monitor");
                ModLogger.Exception(ex);
            }
        }
    }
}
