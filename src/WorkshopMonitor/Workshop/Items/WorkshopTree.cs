using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorkshopMonitor.Overwatch;
using WorkshopMonitor.UI;

namespace WorkshopMonitor.Workshop
{
    public class WorkshopTree : WorkshopAsset
    {
        public WorkshopTree(ulong workshopId, string readableName, string technicalName)
            : base(workshopId, readableName, technicalName)
        { }

        public override AssetType AssetType
        {
            get { return AssetType.Tree; }
        }

        public override Color32 Color
        {
            get { return new Color32(255, 255, 255, 255); }
        }

        public override string SpriteName
        {
            get { return UIConstants.FilterSpriteTrees; }
        }

        internal override void UpdateInstanceCount()
        {
            SetInstanceCount(OverwatchTreeContainer.Instance.GetAssetCount(TechnicalName));
        }
    }
}
