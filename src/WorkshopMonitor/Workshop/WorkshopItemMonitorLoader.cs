using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Workshop
{
    public class WorkshopItemMonitorLoader : LoadingExtensionBase
    {
        private LoadMode _mode;

        public override void OnCreated(ILoading loading)
        {
            ModLogger.Debug("WorkshopItemMonitorLoader created");
        }

        public override void OnReleased()
        {
            ModLogger.Debug("WorkshopItemMonitorLoader Released");
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            _mode = mode;
            
            // Don't start in asset and map editor
            if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
                return;

            try
            {
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

        public override void OnLevelUnloading()
        {
            // Don't stop in asset and map editor
            if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
                return;

            try
            {
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
