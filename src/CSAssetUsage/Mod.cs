using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace CSAssetUsage
{
    public class Mod : IUserMod
    {
        private const string ModName = "Asset Usage";
        private const string ModDescription = "Displays the usage of custom assets in a game";

        public string Name
        {
            get { ModLogger.Debug("bla"); return ModName; }
        }

        public string Description
        {
            get { return ModDescription; }
        }
    }
}
