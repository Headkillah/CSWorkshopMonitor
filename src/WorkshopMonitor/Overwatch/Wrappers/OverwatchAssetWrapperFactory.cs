using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Overwatch
{
    public static class OverwatchAssetWrapperFactory
    {
        public static IOverwatchAssetWrapper CreateAssetWrapper(ushort assetId, object asset)
        {
            if (asset.GetType().Equals(typeof(Building)))
                return new OverwatchBuildingWrapper(assetId, (Building)asset);
            else if (asset.GetType().Equals(typeof(PropInstance)))
                return new OverwatchPropWrapper(assetId, (PropInstance)asset);

            else
                throw new InvalidOperationException("Invalid asset type");
        }
    }
}
