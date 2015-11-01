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
        
        public string Name
        {
            get { return UITexts.ModName; }
        }

        public string Description
        {
            get { return UITexts.ModDescription; }
        }
    }
}
