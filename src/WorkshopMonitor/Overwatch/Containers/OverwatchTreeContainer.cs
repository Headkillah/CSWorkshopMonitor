using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Overwatch
{
    public class OverwatchTreeContainer : OverwatchAssetContainer<OverwatchTreeWrapper, TreeInstance>
    {
        private static OverwatchTreeContainer _instance;

        public static OverwatchTreeContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (_instance == null)
                        _instance = new OverwatchTreeContainer();
                }
                return _instance;
            }
        }

        protected override OverwatchTreeWrapper CreateAssetWrapper(ushort assetId, TreeInstance asset)
        {
            return new OverwatchTreeWrapper(assetId, asset);
        }
    }
}
