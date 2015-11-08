using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    /// <summary>
    /// Represents a class responsible for starting/stopping the WorkshopItemMonitor when a game is started or exited
    /// </summary>
    public class WorkshopItemMonitorLoader : LoadingExtensionBase
    {
        private LoadMode _mode;

        /// <summary>
        /// Called when the WorkshopItemMonitor loader is created
        /// </summary>
        /// <param name="loading">The loading instance</param>
        public override void OnCreated(ILoading loading)
        {
            ModLogger.Debug("WorkshopItemMonitorLoader created");
        }

        /// <summary>
        /// Called when the WorkshopItemMonitor loader is release
        /// </summary>
        public override void OnReleased()
        {
            ModLogger.Debug("WorkshopItemMonitorLoader Released");
        }

        /// <summary>
        /// Called when a new/existing game has been loaded by the user, starts the WorkshopItemMonitor
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
                // Start the WorkshopItemMonitor
                ModLogger.Debug("Starting WorkshopItemMonitor");
                WorkshopItemMonitor.Instance.Start();
                ModLogger.Debug("WorkshopItemMonitor started");
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while starting the WorkshopItemMonitor");
                ModLogger.Exception(ex);
            }
        }

        /// <summary>
        /// Called when the user exits the game, stops the WorkshopItemMonitor
        /// </summary>
        public override void OnLevelUnloading()
        {
            // Don't stop in asset and map editor
            if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
                return;

            try
            {
                // Stop the WorkshopItemMonitor
                ModLogger.Debug("Stopping WorkshopItemMonitor");
                WorkshopItemMonitor.Instance.Stop();
                ModLogger.Debug("WorkshopItemMonitor stopped");
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while stopping the WorkshopItemMonitor");
                ModLogger.Exception(ex);
            }
        }
    }
}
