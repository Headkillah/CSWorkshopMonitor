using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorkshopMonitor.Overwatch;
using WorkshopMonitor.UI;

namespace WorkshopMonitor.Workshop
{
    public class WorkshopProp : WorkshopAsset
    {
        public WorkshopProp(ulong workshopId, string readableName, string technicalName)
            : base(workshopId, readableName, technicalName)
        { }

        public override AssetType AssetType
        {
            get { return AssetType.Prop; }
        }

        public override Color32 Color
        {
            get { return new Color32(255, 255, 255, 255); }
        }

        public override string SpriteName
        {
            get { return UIConstants.FilterSpriteProps; }
        }

        internal override void UpdateInstanceCount()
        {
            SetInstanceCount(OverwatchPropContainer.Instance.GetAssetCount(TechnicalName));
        }
    }
}
