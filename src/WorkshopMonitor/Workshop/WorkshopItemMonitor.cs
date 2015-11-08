using ColossalFramework.Packaging;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WorkshopMonitor
{
    /// <summary>
    /// Represents a class responsible for monitoring the list of available workshop items in the game
    /// </summary>
    public class WorkshopItemMonitor
    {
        private static readonly WorkshopItemMonitor _instance = new WorkshopItemMonitor();

        private List<WorkshopItem> _workshopItems;

        /// <summary>
        /// Prevents a default instance of the <see cref="WorkshopItemMonitor"/> class from being created.
        /// </summary>
        private WorkshopItemMonitor()
        {
            _workshopItems = new List<WorkshopItem>();
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="WorkshopItemMonitor"/> class
        /// </summary>
        public static WorkshopItemMonitor Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Starts the workshop monitor by loading all available workshop items from the CS package manager and matching them with the list of available prefabs in the game
        /// </summary>
        public void Start()
        {
            try
            {
                ModLogger.Debug("WorkshopItemMonitor is loading workshop items");

                // The package manager monitors the list of workshop items, so retrieve the packageid and item name from each item
                var workshopAssets = PackageManager
                   .FilterAssets(UserAssetType.CustomAssetMetaData)
                   .Where(a => a.isWorkshopAsset)
                   .Select(a => new { WorkshopId = ulong.Parse(a.package.packageName), Name = a.Instantiate<CustomAssetMetaData>().name });

                // The PrefabCollection monitors the list of all prefabs available in the game, which includes the default CS prefabs and the custom prefabs from workshop items
                for (int i = 0; i < PrefabCollection<BuildingInfo>.PrefabCount(); i++)
                {
                    BuildingInfo prefab = PrefabCollection<BuildingInfo>.GetPrefab((uint)i);
                    if (prefab != null)
                    {
                        var workshopMatch = Regex.Match(prefab.name, @"^[\d]+");
                        if (workshopMatch.Success)
                        {
                            var workshopId = ulong.Parse(workshopMatch.Value);
                            var workshopAsset = workshopAssets.FirstOrDefault(a => a.WorkshopId == workshopId);
                            if (workshopAsset != null)
                                _workshopItems.Add(new WorkshopItem(workshopId, workshopAsset.Name, GetBuildingType(prefab.GetService())));
                        }
                    }
                }

                ModLogger.Debug("WorkshopItemMonitor loaded {0} workshop items", GetWorkshopItemCount());
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while starting the workshop monitor, no workshop items where loaded");
                ModLogger.Exception(ex);
            }
        }

        /// <summary>
        /// Stops the workshop monitor by clearing all loaded workshop items
        /// </summary>
        public void Stop()
        {
            int itemCount = GetWorkshopItemCount();
            _workshopItems = new List<WorkshopItem>();
            ModLogger.Debug("WorkshopItemMonitor unloaded {0} workshop items", itemCount);
        }

        /// <summary>
        /// Gets number of workshop items loaded by the workshop monitor
        /// </summary>
        /// <returns></returns>
        public int GetWorkshopItemCount()
        {
            return _workshopItems.Count;
        }

        /// <summary>
        /// Gets a list of workshop items loaded by the workshop monitor
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WorkshopItem> GetWorkshopItems()
        {
            return _workshopItems.AsEnumerable();
        }

        /// <summary>
        /// Updates the list of workshop items by recalculating the number of usages for each item in the game
        /// </summary>
        public void Update()
        {
            try
            {
                int workshopItemCount = GetWorkshopItemCount();

                ModLogger.Debug("WorkshopItemMonitor is updating {0} workshop items", workshopItemCount);
                foreach (var item in _workshopItems)
                {
                    item.SetInstanceCount(OverwatchContainer.Instance.GetBuildingCount(item.WorkshopId));
                }
                ModLogger.Debug("WorkshopMonitorr updated {0} workshop items", workshopItemCount);
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while updating the workshop item list, the numbers displayed could be incorrect");
                ModLogger.Exception(ex);
            }
        }

        private BuildingType GetBuildingType(ItemClass.Service service)
        {
            switch (service)
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
                case ItemClass.Service.Road:
                    return BuildingType.Roads;

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
