//using ColossalFramework;
//using ColossalFramework.Packaging;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;

//namespace CSAssetUsage
//{
//    public static class AssetCollector
//    {
//        public static IEnumerable<AssetMetadata> CollectWorkshopAssets()
//        {
//            Stopwatch sw = new Stopwatch();
//            sw.Start();

//            //// Retrieve all active buildings from the building manager, filter on the buildingnames that start with a packageid and group them by this packageid
//            //var activeBuildings = Singleton<BuildingManager>.instance.m_buildings.m_buffer
//            //    .Where(b => b.m_flags > Building.Flags.None)
//            //    .Where(b => Regex.IsMatch(b.Info.name, @"^[\d]+."))
//            //    .GroupBy(b => b.Info.name.Split('.').First()).ToList();

//            // Retrieve all installed custom assets
//            var assets = PackageManager
//                .FilterAssets(UserAssetType.CustomAssetMetaData)
//                .Where(a => a.isWorkshopAsset)
//                .Select(a => new AssetMetadata { PackageId = a.package.packageName, Name = a.name, NumberUsed = a.in })
//                .ToList();

//            //// Combine the list of active buildings and custom assets into a metadata list
//            //foreach (var asset in assets)
//            //{
//            //    var activeBuilding = activeBuildings.FirstOrDefault(b => b.Key == asset.PackageId);
//            //    if (activeBuilding != null)
//            //    {
//            //        // Assign to variable first to prevent lazy-evaluation issues
//            //        int count = activeBuilding.Count();
//            //        asset.NumberUsed = count;
//            //    }
//            //}

//            //var test = Data.Instance.Buildings.First();

//            //var buildingManager = Singleton<BuildingManager>.instance;
//            //var building = buildingManager.m_buildings.m_buffer[Data.Instance.Buildings.First()];
//            //building. CustomAssetMetaData



//            sw.Stop();
//            ModLogger.Debug(sw.ElapsedMilliseconds.ToString());
//            return new AssetMetadata[0];
//        }
//    }
//}
