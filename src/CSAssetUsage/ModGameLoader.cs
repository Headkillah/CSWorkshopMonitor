using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CSAssetUsage
{
    /// <summary>
    /// Represents a class responsible for loading/unloading the AssetUsage main window when a game is started or finished
    /// </summary>
    public class ModGameLoader : LoadingExtensionBase
    {
        private const string AssetUsageMainWindowGameObjectName = "AssetUsageMainWindow";

        private LoadMode _mode;
        private UIMainWindow _mainWindow;

        /// <summary>
        /// Called when a new/existing game has been loaded by the user. Creates the main window object and adds it to CS UI
        /// </summary>
        /// <param name="mode">The mode.</param>
        public sealed override void OnLevelLoaded(LoadMode mode)
        {
            // Store the mode (used when unloading again)
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

        /// <summary>
        /// Called when the user exits the game. Destroys the AssetUsage main window
        /// </summary>
        public override void OnLevelUnloading()
        {
            // Don't unload in asset and map editor
            if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
                return;

            // Destroy the mainwindow when unloading to make sure a fresh new window is used for the next game
            if (_mainWindow != null)
                GameObject.Destroy(_mainWindow.gameObject);
        }
    }
}
