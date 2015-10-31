using ColossalFramework.Packaging;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    public class AssetMonitor
    {
        private static readonly AssetMonitor _instance = new AssetMonitor();

        private AssetEntry[] _assets;

        private AssetMonitor()
        {
            _assets = new AssetEntry[0];
        }

        public static AssetMonitor Instance
        {
            get { return _instance; }
        }

        public void Load()
        {
            _assets = PackageManager
               .FilterAssets(UserAssetType.CustomAssetMetaData)
               .Where(a => a.isWorkshopAsset)
               .Select(a => new { PackageId = a.package.packageName, Metadata = a.Instantiate<CustomAssetMetaData>() })
               .Where(a => a.Metadata.type == CustomAssetMetaData.Type.Building)
               .Select(a => new AssetEntry (a.PackageId, a.Metadata ))
               .ToArray();
        }

        public int GetAssetCount()
        {
            return _assets.Length;
        }

        public List<AssetEntry> GetLoadedAssets()
        {
            return _assets.ToList();
        }

        public void Update()
        {
            foreach (var asset in _assets)
            {
                asset.SetInstanceCount(OverwatchData.Instance.GetBuildingCount(asset.PackageId));
            }
        }
    }
}
