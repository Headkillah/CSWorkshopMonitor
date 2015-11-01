using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CSAssetUsage
{
    public class ModLoader : LoadingExtensionBase
    {
        private const string AssetUsageMainWindowGameObjectName = "AssetUsageMainWindow";

        private LoadMode _mode;
        private UIMainWindow _mainWindow;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
//#if DEBUG
            ModLogger.EnableDebugLogging();
//#endif
        }

        public sealed override void OnLevelLoaded(LoadMode mode)
        {
            // Store the mode (used when unloading again)
            _mode = mode;

            // don't load mod in asset and map editor
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
                return;

            try
            {
                ModLogger.Debug("Creating main window");

                // Get a handle to the main game view
                UIView aView = UIView.GetAView();

                // Create the gameobject and attach a mainwindow instance
                GameObject goMainWindow = new GameObject(AssetUsageMainWindowGameObjectName);
                _mainWindow = goMainWindow.AddComponent<UIMainWindow>();
                _mainWindow.transform.parent = aView.transform;

                ModLogger.Debug("Main window created");
            }
            catch (Exception ex)
            {
                ModLogger.Error(ex.ToString());
            }
        }

        public override void OnLevelUnloading()
        {
            if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
                return;

            // Destroy the mainwindow when unloading to make sure a fresh new window is used for the next game
            if (_mainWindow != null)
                GameObject.Destroy(_mainWindow.gameObject);
        }
    }
}
