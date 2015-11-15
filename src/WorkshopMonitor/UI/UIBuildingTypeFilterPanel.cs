using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorkshopMonitor.Overwatch;

namespace WorkshopMonitor.UI
{
    public class UIBuildingTypeFilterPanel : UIPanel
    {
        public event EventHandler<FilterChangedEventArgs> FilterChanged;

        private List<UIBuildingTypeFilterOption> _filterOptions;
        private BuildingType _currentFilter;

        public override void Start()
        {
            base.Start();

            _filterOptions = new List<UIBuildingTypeFilterOption>();

            width = UIConstants.FilterPanelWidth;
            height = UIConstants.FilterPanelHeight;

            backgroundSprite = UIConstants.FilterPanelBackgroundSprite;

            AddButtons();

            int offset = UIConstants.FilterFirstOptionXOffset;
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
            offset = AddFilterOption(offset, BuildingType.Roads);
            offset += UIConstants.FilterGroupedOptionsOffset;
            offset = AddFilterOption(offset, BuildingType.Residential);
            offset = AddFilterOption(offset, BuildingType.Commercial);
            offset = AddFilterOption(offset, BuildingType.Industrial);
            offset = AddFilterOption(offset, BuildingType.Office);
            offset += UIConstants.FilterGroupedOptionsOffset;
            offset = AddFilterOption(offset, BuildingType.Other);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            if (_filterOptions != null)
            {
                _filterOptions.ForEach(c =>
                {
                    if (c != null)
                        c.CheckedChanged -= FilterOption_CheckedChanged;
                });
            }
        }

        private int AddFilterOption(int offset, BuildingType buildingType)
        {
            var filterOption = AddUIComponent<UIBuildingTypeFilterOption>();
            filterOption.relativePosition = new Vector3(offset, UIConstants.FilterOptionYOffset);
            filterOption.Initialize(buildingType);
            filterOption.CheckedChanged += FilterOption_CheckedChanged;
            _filterOptions.Add(filterOption);
            _currentFilter = _currentFilter | buildingType;
            return offset + (int)filterOption.width;
        }

        private void AddButtons()
        {
            var selectAllButton = createButton();
            selectAllButton.text = UITexts.FilterButtonSelectAllText;
            selectAllButton.eventClick += SelectAllButton_eventClick;
            selectAllButton.relativePosition = new Vector3(UIConstants.FilterPanelWidth - UIConstants.FilterSelectAllButtonXOffset - selectAllButton.width, UIConstants.FilterButtonYOffset); 

            var selectNoneButton = createButton();
            selectNoneButton.text = UITexts.FilterButtonSelectNoneText;
            selectNoneButton.eventClick += SelectNoneButton_eventClick;
            selectNoneButton.relativePosition = new Vector3(selectAllButton.relativePosition.x - UIConstants.FilterSelectNoneButtonXOffset -  selectNoneButton.width, UIConstants.FilterButtonYOffset);
        }

        private UIButton createButton()
        {
            var result = AddUIComponent<UIButton>();
            result.textScale = UIConstants.FilterButtonTextScale;
            result.normalBgSprite = UIConstants.FilterButtonNormalSprite;
            result.hoveredBgSprite = UIConstants.FilterButtonHoveredSprite;
            result.focusedBgSprite = UIConstants.FilterButtonNormalSprite;
            result.pressedBgSprite = UIConstants.FilterButtonNormalSprite;
            result.size = new Vector2(UIConstants.FilterButtenWidth, UIConstants.FilterButtenHeight);
            return result;
        }

        private void FilterOption_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Checked)
                _currentFilter |= e.BuildingType;
            else
                _currentFilter &= ~e.BuildingType;

            OnFilterChanged();
        }

        private void SelectAllButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            applyFilterSelectAction(BuildingType.All);
        }

        private void SelectNoneButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            applyFilterSelectAction(BuildingType.None);
        }

        private void applyFilterSelectAction(BuildingType filter)
        {
            _filterOptions.ForEach(o => o.SetCheckedSilent(filter == BuildingType.All));
            _currentFilter = filter;
            OnFilterChanged();
        }

        protected virtual void OnFilterChanged()
        {
            var handler = this.FilterChanged;
            if (handler != null)
                handler.Invoke(this, new FilterChangedEventArgs(_currentFilter));
        }

        private enum ButtonAction
        {
            SelectAll,
            SelectNone
        }
    }
}
