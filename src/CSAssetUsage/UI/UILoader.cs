using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;

namespace CSAssetUsage
{
    /// <summary>
    /// Represents a class responsible for loading/unloading the AssetUsage main window when a game is started or exited
    /// </summary>
    public class UILoader : LoadingExtensionBase
    {
        private const string AssetUsageMainWindowGameObjectName = "AssetUsageMainWindow";

        private LoadMode _mode;
        private UIMainWindow _mainWindow;

        /// <summary>
        /// Called when the UI loader is created
        /// </summary>
        /// <param name="loading">The loading instance</param>
        public override void OnCreated(ILoading loading)
        {
            ModLogger.Debug("UILoader created");
        }

        /// <summary>
        /// Called when the UI loader is release
        /// </summary>
        public override void OnReleased()
        {
            ModLogger.Debug("UILoader Released");
        }

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
                ModLogger.Error("An error occured while creating the main window");
                ModLogger.Exception(ex);
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
