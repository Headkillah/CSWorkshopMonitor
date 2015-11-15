using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;

namespace WorkshopMonitor.UI
{
    public class UILoader : LoadingExtensionBase
    {
        private const string WorkshopMonitorMainWindowGameObjectName = "WorkshopMonitorMainWindow";

        private LoadMode _mode;
        private UIMainWindow _mainWindow;

        public override void OnCreated(ILoading loading)
        {
            ModLogger.Debug("UILoader created");
        }

        public override void OnReleased()
        {
            ModLogger.Debug("UILoader Released");
        }

        public sealed override void OnLevelLoaded(LoadMode mode)
        {
            _mode = mode;

            // Don't load in asset and map editor
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
                return;

            try
            {
                ModLogger.Debug("Creating main window");

                // Get a handle to the main game view
                UIView aView = UIView.GetAView(); 

                // Create the gameobject and attach a mainwindow instance
                GameObject goMainWindow = new GameObject(WorkshopMonitorMainWindowGameObjectName);
                _mainWindow = goMainWindow.AddComponent<UIMainWindow>();
                _mainWindow.transform.parent = aView.transform;

                ModLogger.Debug("Main window created");
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while creating the main window");
                ModLogger.Exception(ex);
            }
        }

        public override void OnLevelUnloading()
        {
            // Don't unload in asset and map editor
            if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
                return;

            try
            {
                ModLogger.Debug("Destroying main window");

                // Destroy the mainwindow when unloading to make sure a fresh new window is used for the next game
                if (_mainWindow != null)
                    GameObject.Destroy(_mainWindow.gameObject);

                ModLogger.Debug("Main window destroyed");
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while destroying the main window");
                ModLogger.Exception(ex);
            }
        }
    }
}