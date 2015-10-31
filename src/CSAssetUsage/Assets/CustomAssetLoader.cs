using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    public class CustomAssetLoader : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode == LoadMode.NewGame || mode == LoadMode.LoadGame)
            {
                AssetMonitor.Instance.Load();
                ModLogger.Debug("Loaded {0} custom assets", AssetMonitor.Instance.GetAssetCount());
            }

        }
    }
}
