using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorkshopMonitor.Overwatch;

namespace WorkshopMonitor.UI
{
    public class UIAssetTypeFilterPanel : UIPanel
    {
        public event EventHandler<FilterChangedEventArgs> FilterChanged;

        private List<UIAssetTypeFilterOption> _filterOptions;
        private AssetType _currentFilter;

        public override void Start()
        {
            base.Start();

            _filterOptions = new List<UIAssetTypeFilterOption>();

            width = UIConstants.FilterPanelWidth;
            height = UIConstants.FilterPanelHeight;

            backgroundSprite = UIConstants.FilterPanelBackgroundSprite;

            AddButtons();

            int offset = UIConstants.FilterFirstOptionXOffset;
            offset += UIConstants.FilterGroupedOptionsOffset;
            offset = AddFilterOption(offset, AssetType.Electricity);
            offset = AddFilterOption(offset, AssetType.WaterAndSewage);
            offset = AddFilterOption(offset, AssetType.Garbage);
            offset = AddFilterOption(offset, AssetType.Healthcare);
            offset = AddFilterOption(offset, AssetType.FireDepartment);
            offset = AddFilterOption(offset, AssetType.Police);
            offset = AddFilterOption(offset, AssetType.Education);
            offset = AddFilterOption(offset, AssetType.PublicTransport);
            offset = AddFilterOption(offset, AssetType.Beautification);
            offset = AddFilterOption(offset, AssetType.Monuments);
            offset = AddFilterOption(offset, AssetType.Roads);
            offset += UIConstants.FilterGroupedOptionsOffset;
            offset = AddFilterOption(offset, AssetType.Residential);
            offset = AddFilterOption(offset, AssetType.Commercial);
            offset = AddFilterOption(offset, AssetType.Industrial);
            offset = AddFilterOption(offset, AssetType.Office);
            offset += UIConstants.FilterGroupedOptionsOffset;
            offset = AddFilterOption(offset, AssetType.Prop);
            offset += UIConstants.FilterGroupedOptionsOffset;
            offset = AddFilterOption(offset, AssetType.Other);
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

        private int AddFilterOption(int offset, AssetType assetType)
        {
            var filterOption = AddUIComponent<UIAssetTypeFilterOption>();
            filterOption.relativePosition = new Vector3(offset, UIConstants.FilterOptionYOffset);
            filterOption.Initialize(assetType);
            filterOption.CheckedChanged += FilterOption_CheckedChanged;
            _filterOptions.Add(filterOption);
            _currentFilter = _currentFilter | assetType;
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
                _currentFilter |= e.AssetType;
            else
                _currentFilter &= ~e.AssetType;

            OnFilterChanged();
        }

        private void SelectAllButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            applyFilterSelectAction(AssetType.All);
        }

        private void SelectNoneButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            applyFilterSelectAction(AssetType.None);
        }

        private void applyFilterSelectAction(AssetType filter)
        {
            _filterOptions.ForEach(o => o.SetCheckedSilent(filter == AssetType.All));
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
