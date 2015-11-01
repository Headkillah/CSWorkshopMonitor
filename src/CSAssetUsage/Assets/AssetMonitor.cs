using ColossalFramework.Packaging;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    /// <summary>
    /// Represents a class responsible for monitoring the list of available custom assets in the game
    /// </summary>
    public class AssetMonitor
    {
        private static readonly AssetMonitor _instance = new AssetMonitor();

        private AssetEntry[] _assets;

        /// <summary>
        /// Prevents a default instance of the <see cref="AssetMonitor"/> class from being created.
        /// </summary>
        private AssetMonitor()
        {
            _assets = new AssetEntry[0];
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="AssetMonitor"/> class
        /// </summary>
        public static AssetMonitor Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Starts the asset monitor by loading all available custom assets from the CS package manager
        /// </summary>
        public void Start()
        {
            try
            {
                ModLogger.Debug("AssetMonitor is loading custom assets");

                _assets = PackageManager
                   .FilterAssets(UserAssetType.CustomAssetMetaData)
                   .Where(a => a.isWorkshopAsset)
                   .Select(a => new { PackageId = a.package.packageName, Metadata = a.Instantiate<CustomAssetMetaData>() })
                   .Where(a => a.Metadata.type == CustomAssetMetaData.Type.Building)
                   .Select(a => new AssetEntry(a.PackageId, a.Metadata))
                   .ToArray();

                ModLogger.Debug("AssetMonitor loaded {0} custom assets", GetAssetCount());
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while starting the asset monitor, no assets where loaded");
                ModLogger.Exception(ex);
            }
        }

        /// <summary>
        /// Stops the asset monitor by clearing all loaded custom assets
        /// </summary>
        public void Stop()
        {
            int assetCount = GetAssetCount();
            _assets = new AssetEntry[0];
            ModLogger.Debug("AssetMonitor unloaded {0} custom assets", assetCount);
        }

        /// <summary>
        /// Gets number of custom assets loaded by the asset monitor
        /// </summary>
        /// <returns></returns>
        public int GetAssetCount()
        {
            return _assets.Length;
        }

        /// <summary>
        /// Gets a list of assets loaded by the asset monitor
        /// </summary>
        /// <returns></returns>
        public List<AssetEntry> GetAssetList()
        {
            return _assets.ToList();
        }

        /// <summary>
        /// Updates the list of assets by recalculating the number of usages for each asset in the game
        /// </summary>
        public void Update()
        {
            try
            {
                ModLogger.Debug("Asset monitor is updating {0} assets", GetAssetCount());

                foreach (var asset in _assets)
                {
                    asset.SetInstanceCount(OverwatchData.Instance.GetBuildingCount(asset.PackageId));
                }

                ModLogger.Debug("Asset monitor updated {0} assets", GetAssetCount());
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while updating the asset list, the numbers displayed could be incorrect");
                ModLogger.Exception(ex);
            }
        }
    }
}
