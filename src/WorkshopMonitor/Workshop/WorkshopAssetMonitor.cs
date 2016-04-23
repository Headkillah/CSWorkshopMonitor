using ColossalFramework.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WorkshopMonitor.Overwatch;
using WorkshopMonitor.Extensions;

namespace WorkshopMonitor.Workshop
{
    public class WorkshopAssetMonitor
    {
        private static readonly WorkshopAssetMonitor _instance = new WorkshopAssetMonitor();

        private List<WorkshopAsset> _workshopAssets;

        /// <summary>
        /// Prevents a default instance of the <see cref="WorkshopAssetMonitor"/> class from being created.
        /// </summary>
        private WorkshopAssetMonitor()
        {
            _workshopAssets = new List<WorkshopAsset>();
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="WorkshopAssetMonitor"/> class
        /// </summary>
        public static WorkshopAssetMonitor Instance
        {
            get { return _instance; }
        }

        public void Start()
        {
            try
            {
                ModLogger.Debug("WorkshopAssetMonitor is loading workshop assets");

                // The package manager monitors the list of workshop assets, so retrieve the packageid from each item
                var workshopIds = PackageManager
                   .FilterAssets(UserAssetType.CustomAssetMetaData)
                   .Where(a => a.isWorkshopAsset)
                   .Select(a => new { Asset = a, Metadata = a.Instantiate<CustomAssetMetaData>() })
                   .Select(a => ulong.Parse(a.Asset.package.packageName))
                   .Distinct();

                // The PrefabCollections monitor the list of all prefabs available in the game, which includes the default CS prefabs and the custom prefabs from workshop assets
                // Try to match the prefabs with the workshop packageid list to make sure only workshopassets are loaded.

                for (int i = 0; i < PrefabCollection<PropInfo>.PrefabCount(); i++)
                {
                    PropInfo propPrefab = PrefabCollection<PropInfo>.GetPrefab((uint)i);
                    if (propPrefab != null)
                    {
                        var workshopPropMatch = Regex.Match(propPrefab.name, RegexExpression.BuildingName, RegexOptions.IgnoreCase);
                        if (workshopPropMatch.Success)
                        {
                            var workshopPropId = ulong.Parse(workshopPropMatch.Groups["packageid"].Value);
                            if (workshopIds.Any(id => id == workshopPropId))
                                _workshopAssets.Add(new WorkshopProp(workshopPropId, workshopPropMatch.Groups["prefabname"].Value, propPrefab.name));
                        }
                    }
                }

                for (int i = 0; i < PrefabCollection<TreeInfo>.PrefabCount(); i++)
                {
                    TreeInfo treePrefab = PrefabCollection<TreeInfo>.GetPrefab((uint)i);
                    if (treePrefab != null)
                    {
                        var workshopTreeMatch = Regex.Match(treePrefab.name, RegexExpression.BuildingName, RegexOptions.IgnoreCase);
                        if (workshopTreeMatch.Success)
                        {
                            var workshopPropId = ulong.Parse(workshopTreeMatch.Groups["packageid"].Value);
                            if (workshopIds.Any(id => id == workshopPropId))
                                _workshopAssets.Add(new WorkshopTree(workshopPropId, workshopTreeMatch.Groups["prefabname"].Value, treePrefab.name));
                        }
                    }
                }
                
                for (int i = 0; i < PrefabCollection<BuildingInfo>.PrefabCount(); i++)
                {
                    BuildingInfo buildingPrefab = PrefabCollection<BuildingInfo>.GetPrefab((uint)i);
                    if (buildingPrefab != null)
                    {
                        var workshopBuildingMatch = Regex.Match(buildingPrefab.name, RegexExpression.BuildingName, RegexOptions.IgnoreCase);
                        if (workshopBuildingMatch.Success)
                        {
                            var workshopBuildingId = ulong.Parse(workshopBuildingMatch.Groups["packageid"].Value);
                            if (workshopIds.Any(id => id == workshopBuildingId))
                                _workshopAssets.Add(new WorkshopBuilding(workshopBuildingId, workshopBuildingMatch.Groups["prefabname"].Value, buildingPrefab.name, buildingPrefab.GetService().ToAssetType()));
                        }
                    }
                }

                

                ModLogger.Debug("WorkshopAssetMonitor loaded {0} workshop assets", GetWorkshopAssetCount());
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while starting the workshop monitor, no workshop assets where loaded");
                ModLogger.Exception(ex);
            }
        }

        public void Stop()
        {
            var workshopAssetCount = _workshopAssets.Count;

            _workshopAssets = new List<WorkshopAsset>();

            ModLogger.Debug("WorkshopAssetMonitor unloaded {0} workshop assets", workshopAssetCount);
        }

        public int GetWorkshopAssetCount()
        {
            return _workshopAssets.Count;
        }

        public IEnumerable<WorkshopAsset> GetWorkshopAssets()
        {
            return _workshopAssets.AsEnumerable();
        }

        public void Remove(Guid workshopAssetId)
        {
            _workshopAssets.RemoveAll(w => w.WorkshopAssetId == workshopAssetId);
        }

        public void Update()
        {
            try
            {
                int workshopAssetCount = _workshopAssets.Count();

                ModLogger.Debug("WorkshopAssetMonitor is updating {0} workshop assets", workshopAssetCount);
                foreach (var item in _workshopAssets)
                {
                    item.UpdateInstanceCount();
                }
                ModLogger.Debug("WorkshopMonitor updated {0} workshop assets", workshopAssetCount);

            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while updating the workshop item list, the numbers displayed could be incorrect");
                ModLogger.Exception(ex);
            }
        }


    }
}
