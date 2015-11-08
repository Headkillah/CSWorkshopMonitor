using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CSAssetUsage
{
    public class UIFilterPanel : UIPanel
    {
        public event EventHandler<FilterChangedEventArgs> FilterChanged;

        private List<UIFilterOption> _filterOptions;
        private BuildingType _currentFilter;

        public override void Start()
        {
            base.Start();

            _filterOptions = new List<UIFilterOption>();

            width = UIConstants.FilterPanelWidth;
            height = UIConstants.FilterPanelHeight;

            //backgroundSprite = "CursorInfoBack";
            backgroundSprite = "CursorInfoBack";

            int offset = UIConstants.FilterFirstOptionXOffset;
            //offset = AddFilterOption(offset, BuildingType.All, UIConstants.FilterSpriteAll);
            offset += UIConstants.FilterGroupedOptionsOffset;
            offset = AddFilterOption(offset, BuildingType.Electricity);
            offset = AddFilterOption(offset, BuildingType.WaterAndSewage);
            offset = AddFilterOption(offset, BuildingType.Garbage);
            offset = AddFilterOption(offset, BuildingType.Healthcare);
            offset = AddFilterOption(offset, BuildingType.FireDepartment);
            offset = AddFilterOption(offset, BuildingType.Police);
            offset = AddFilterOption(offset, BuildingType.Education);
            offset = AddFilterOption(offset, BuildingType.PublicTransport);
            offset = AddFilterOption(offset, BuildingType.Beautification);
            offset = AddFilterOption(offset, BuildingType.Monuments);
            offset += UIConstants.FilterGroupedOptionsOffset;
            offset = AddFilterOption(offset, BuildingType.Residential);
            offset = AddFilterOption(offset, BuildingType.Commercial);
            offset = AddFilterOption(offset, BuildingType.Industrial);
            offset = AddFilterOption(offset, BuildingType.Office);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _filterOptions.ForEach(c => c.CheckedChanged -= FilterOption_CheckedChanged);
        }

        private int AddFilterOption(int offset, BuildingType buildingType)
        {
            var filterOption = AddUIComponent<UIFilterOption>();
            filterOption.relativePosition = new Vector3(offset, UIConstants.FilterOptionYOffset);
            filterOption.Initialize(buildingType);
            filterOption.CheckedChanged += FilterOption_CheckedChanged;
            _currentFilter = _currentFilter | buildingType;
            return offset + (int)filterOption.width;
        }

        private void FilterOption_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Checked)
                _currentFilter |= e.BuildingType;
            else
                _currentFilter &= ~e.BuildingType;
            OnFilterChanged();
        }

        protected virtual void OnFilterChanged()
        {
            var handler = this.FilterChanged;
            if (handler != null)
                handler.Invoke(this, new FilterChangedEventArgs(_currentFilter));
        }
    }
}
