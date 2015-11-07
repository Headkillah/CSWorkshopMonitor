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
                   .Select(a => new AssetEntry(a.PackageId, a.Metadata, GetBuildingType(a.Metadata)))
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
        public IEnumerable<AssetEntry> GetAssets()
        {
            return _assets;
        }

        /// <summary>
        /// Updates the list of assets by recalculating the number of usages for each asset in the game
        /// </summary>
        public void Update()
        {
            try
            {
                int assetCount = GetAssetCount();

                ModLogger.Debug("Asset monitor is updating {0} assets", assetCount);
                foreach (var asset in _assets)
                {
                    asset.SetInstanceCount(OverwatchContainer.Instance.GetBuildingCount(asset.PackageId));
                }
                ModLogger.Debug("Asset monitor updated {0} assets", assetCount);
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while updating the asset list, the numbers displayed could be incorrect");
                ModLogger.Exception(ex);
            }
        }
        
        private BuildingType GetBuildingType(CustomAssetMetaData metadata)
        {
            var result = GetBuildingTypeFromService(metadata);
            if (result == BuildingType.None)
                result = GetBuildingTypeFromTags(metadata);
            return result;
        }

        private BuildingType GetBuildingTypeFromTags(CustomAssetMetaData metadata)
        {
            if (metadata.steamTags.Intersect(AssetConstants.TagsResidential).Any())
                return BuildingType.Residential;
            if (metadata.steamTags.Intersect(AssetConstants.TagsCommercial).Any())
                return BuildingType.Commercial;
            if (metadata.steamTags.Intersect(AssetConstants.TagsIndustrial).Any())
                return BuildingType.Industrial;
            if (metadata.steamTags.Intersect(AssetConstants.TagsOffice).Any())
                return BuildingType.Office;
            if (metadata.steamTags.Intersect(AssetConstants.TagsElectricity).Any())
                return BuildingType.Electricity;
            if (metadata.steamTags.Intersect(AssetConstants.TagsWaterAndSewage).Any())
                return BuildingType.WaterAndSewage;
            if (metadata.steamTags.Intersect(AssetConstants.TagsGarbage).Any())
                return BuildingType.Garbage;
            if (metadata.steamTags.Intersect(AssetConstants.TagsHealthcare).Any())
                return BuildingType.Healthcare;
            if (metadata.steamTags.Intersect(AssetConstants.TagsFireDepartment).Any())
                return BuildingType.FireDepartment;
            if (metadata.steamTags.Intersect(AssetConstants.TagsPoliceDepartment).Any())
                return BuildingType.Police;
            if (metadata.steamTags.Intersect(AssetConstants.TagsEducation).Any())
                return BuildingType.Education;
            if (metadata.steamTags.Intersect(AssetConstants.TagsPublicTransport).Any())
                return BuildingType.PublicTransport;
            if (metadata.steamTags.Intersect(AssetConstants.TagsBeautification).Any())
                return BuildingType.Beautification;
            if (metadata.steamTags.Intersect(AssetConstants.TagsMonument).Any())
                return BuildingType.Monuments;

            return BuildingType.None;
        }

        private BuildingType GetBuildingTypeFromService(CustomAssetMetaData metadata)
        {
            switch (metadata.service)
            {
                case ItemClass.Service.None:
                    return BuildingType.None;
                case ItemClass.Service.Residential:
                    return BuildingType.Residential;
                case ItemClass.Service.Commercial:
                    return BuildingType.Commercial;
                case ItemClass.Service.Industrial:
                    return BuildingType.Industrial;
                case ItemClass.Service.Office:
                    return BuildingType.Office;
                case ItemClass.Service.Electricity:
                    return BuildingType.Electricity;
                case ItemClass.Service.Water:
                    return BuildingType.WaterAndSewage;
                case ItemClass.Service.Beautification:
                    return BuildingType.Beautification;
                case ItemClass.Service.Garbage:
                    return BuildingType.Garbage;
                case ItemClass.Service.HealthCare:
                    return BuildingType.Healthcare;
                case ItemClass.Service.PoliceDepartment:
                    return BuildingType.Police;
                case ItemClass.Service.Education:
                    return BuildingType.Education;
                case ItemClass.Service.Monument:
                    return BuildingType.Monuments;
                case ItemClass.Service.FireDepartment:
                    return BuildingType.FireDepartment;
                case ItemClass.Service.PublicTransport:
                    return BuildingType.PublicTransport;

                case ItemClass.Service.Unused1:
                    return BuildingType.Other;
                case ItemClass.Service.Unused2:
                    return BuildingType.Other;

                case ItemClass.Service.Citizen:
                    return BuildingType.Other;
                case ItemClass.Service.Tourism:
                    return BuildingType.Other;
                case ItemClass.Service.Government:
                    return BuildingType.Other;

                default:
                    return BuildingType.None;
            }
        }
    }
}
