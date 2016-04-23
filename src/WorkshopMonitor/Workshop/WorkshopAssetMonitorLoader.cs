using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Workshop
{
    public class WorkshopAssetMonitorLoader : LoadingExtensionBase
    {
        private LoadMode _mode;

        public override void OnCreated(ILoading loading)
        {
            ModLogger.Debug("WorkshopAssetMonitorLoader created");
        }

        public override void OnReleased()
        {
            ModLogger.Debug("WorkshopAssetMonitorLoader Released");
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            _mode = mode;
            
            // Don't start in asset and map editor
            if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
                return;

            try
            {
                ModLogger.Debug("Starting WorkshopAssetMonitor");
                WorkshopAssetMonitor.Instance.Start();
                ModLogger.Debug("WorkshopAssetMonitor started");
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while starting the WorkshopAssetMonitor");
                ModLogger.Exception(ex);
            }
        }

        public override void OnLevelUnloading()
        {
            // Don't stop in asset and map editor
            if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
                return;

            try
            {
                ModLogger.Debug("Stopping WorkshopAssetMonitor");
                WorkshopAssetMonitor.Instance.Stop();
                ModLogger.Debug("WorkshopAssetMonitor stopped");
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while stopping the WorkshopAssetMonitor");
                ModLogger.Exception(ex);
            }
        }
    }
}
