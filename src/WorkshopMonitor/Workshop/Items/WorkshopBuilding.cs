using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorkshopMonitor.Overwatch;
using WorkshopMonitor.UI;

namespace WorkshopMonitor.Workshop
{
    public class WorkshopBuilding : WorkshopAsset
    {
        private AssetType _assetType;

        public WorkshopBuilding(ulong workshopId, string readableName, string technicalName, AssetType assetType)
            : base(workshopId, readableName, technicalName)
        {
            _assetType = assetType;
        }

        public override AssetType AssetType
        {
            get { return _assetType; }
        }

        public override Color32 Color
        {
            get { return UIConstants.GetAssetTypeColor(_assetType); }
        }

        public override string SpriteName
        {
            get { return  UIConstants.GetAssetTypeSprite(_assetType); }
        }

        internal override void UpdateInstanceCount()
        {
            SetInstanceCount(OverwatchBuildingContainer.Instance.GetAssetCount(TechnicalName));

        }
    }
}
