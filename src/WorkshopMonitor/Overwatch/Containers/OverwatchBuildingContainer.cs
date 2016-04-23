using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Overwatch
{
    public class OverwatchBuildingContainer : OverwatchAssetContainer<OverwatchBuildingWrapper, Building>
    {
        private static OverwatchBuildingContainer _instance;

        public static OverwatchBuildingContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (_instance == null)
                        _instance = new OverwatchBuildingContainer();
                }
                return _instance;
            }
        }

        protected override OverwatchBuildingWrapper CreateAssetWrapper(ushort assetId, Building asset)
        {
            return new OverwatchBuildingWrapper(assetId, asset);
        }

        public override void CacheAsset(ushort assetId, Building asset)
        {
            base.CacheAsset(assetId, asset);

            if (asset.Info == null)
            {
                ModLogger.Debug(string.Format("Asset with id {0} has no info", assetId));
                return;
            }

            if (asset.Info.m_props == null)
            {
                ModLogger.Debug(string.Format("Info of asset with id {0} has no props", assetId));
                return;
            }

            for (var i = 0; i < asset.Info.m_props.Length; i++)
            {

                var prop = asset.Info.m_props[i];
                if (prop == null)
                {
                    ModLogger.Debug(string.Format("Asset with id {0} has null-prop at index {1}", assetId, i));
                    return;
                }
                if (prop.m_prop != null)
                {
                    var wrapper = new OverwatchPropWrapper(assetId, i, prop.m_prop);
                    ModLogger.Debug("caching child prop " + wrapper.TechnicalName);
                    OverwatchPropContainer.Instance.CacheChildAsset(wrapper);
                }
            }
        }
    }
}
