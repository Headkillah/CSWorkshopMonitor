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
            offset = AddFilterOption(offset, BuildingType.Electricity, UIConstants.FilterSpriteElectricity);
            offset = AddFilterOption(offset, BuildingType.WaterAndSewage, UIConstants.FilterSpriteWaterAndSewage);
            offset = AddFilterOption(offset, BuildingType.Garbage, UIConstants.FilterSpriteGarbage);
            offset = AddFilterOption(offset, BuildingType.Healthcare, UIConstants.FilterSpriteHealthcare);
            offset = AddFilterOption(offset, BuildingType.FireDepartment, UIConstants.FilterSpriteFireDepartment);
            offset = AddFilterOption(offset, BuildingType.Police, UIConstants.FilterSpritePolice);
            offset = AddFilterOption(offset, BuildingType.Education, UIConstants.FilterSpriteEducation);
            offset = AddFilterOption(offset, BuildingType.PublicTransport, UIConstants.FilterSpritePublicTransport);
            offset = AddFilterOption(offset, BuildingType.Beautification, UIConstants.FilterSpriteBeautification);
            offset = AddFilterOption(offset, BuildingType.Monuments, UIConstants.FilterSpriteMonuments);
            offset += UIConstants.FilterGroupedOptionsOffset;
            offset = AddFilterOption(offset, BuildingType.Residential, UIConstants.FilterSpriteResidential, UIConstants.FilterColorResidential);
            offset = AddFilterOption(offset, BuildingType.Commercial, UIConstants.FilterSpriteCommercial, UIConstants.FilterColorCommercial);
            offset = AddFilterOption(offset, BuildingType.Industrial, UIConstants.FilterSpriteIndustrial, UIConstants.FilterColorIndustrial);
            offset = AddFilterOption(offset, BuildingType.Office, UIConstants.FilterSpriteOffice, UIConstants.FilterColorOffice);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _filterOptions.ForEach(c => c.CheckedChanged -= FilterOption_CheckedChanged);
        }

        private int AddFilterOption(int offset, BuildingType buildingType, string sprite, Color32? iconColor = null)
        {
            var filterOption = AddUIComponent<UIFilterOption>();
            filterOption.relativePosition = new Vector3(offset, UIConstants.FilterOptionYOffset);
            filterOption.Initialize(buildingType, sprite, iconColor);
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
