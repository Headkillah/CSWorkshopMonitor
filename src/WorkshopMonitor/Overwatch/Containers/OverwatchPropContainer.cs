using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Overwatch
{
    public class OverwatchPropContainer : OverwatchAssetContainer<OverwatchPropWrapper, PropInstance>
    {
        private static OverwatchPropContainer _instance;

        public static OverwatchPropContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (_instance == null)
                        _instance = new OverwatchPropContainer();
                }
                return _instance;
            }
        }

        protected override OverwatchPropWrapper CreateAssetWrapper(ushort assetId, PropInstance asset)
        {
            return new OverwatchPropWrapper(assetId, asset);
        }
    }
}
