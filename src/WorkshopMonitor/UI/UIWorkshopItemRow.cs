/*
    The MIT License (MIT)

    Copyright (c) 2015 Tobias Schwackenhofer

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.

    https://github.com/justacid/Skylines-ExtendedPublicTransport
*/

using ColossalFramework.UI;
using System;
using ColossalFramework;
using UnityEngine;
using System.Diagnostics;
using ColossalFramework.Steamworks;
using WorkshopMonitor.Workshop;

namespace WorkshopMonitor.UI
{
    public class UIWorkshopItemRow : UIPanel
    {
        private UISprite _workshopItemTypeIcon;
        private UILabel _workshopItemNameLabel;
        private UILabel _numberUseLabel;
        private UICustomButton _workshopItemShowInfoButton;
        private UICustomButton _workshopItemUnsubscribeButton;

        private WorkshopItem _workshopItem;

        public ulong WorkshopId
        {
            get
            {
                ulong result = 0;
                if (_workshopItem != null)
                    result = _workshopItem.WorkshopId;
                return result;
            }
        }

        public override void Awake()
        {
            base.Awake();

            height = UIConstants.WorkshopItemRowHeight;
            width = UIConstants.WorkshopItemRowWidth;

            _workshopItemTypeIcon = CreateCellIcon(UIConstants.WorkshopItemTypeIconXOffset, UIConstants.WorkshopItemTypeYOffset);
            _workshopItemNameLabel = CreateCellLabel(UIConstants.WorkshopItemNameLabelXOffset, UIConstants.LabelYOffset, string.Empty);
            _numberUseLabel = CreateCellLabel(UIConstants.NumberUsedLabelXOffset, UIConstants.LabelYOffset, 0.ToString());
            _workshopItemShowInfoButton = CreateInfoCellButton();
            _workshopItemUnsubscribeButton = CreateUnsubscribeCellButton();

            // zebra stripes background
            backgroundSprite = UIConstants.WorkshopItemRowBackgroundSprite;
        }

        /// <summary>
        /// Invoked by the unity engine
        /// </summary>
        public override void OnDestroy()
        {
            // Make sure eventhandlers are destroyed when the panel is destroyed by unity
            if (_workshopItem != null)
                _workshopItem.InstanceCountUpdated -= WorkshopItemEntry_InstanceCountUpdated;
            base.OnDestroy();
        }

        /// <summary>
        /// Loads the specified workshop item into the workshopitemrow
        /// </summary>
        /// <param name="workshopItem">The workshop item entry.</param>
        public void Load(WorkshopItem workshopItem, bool isOdd)
        {
            _workshopItem = workshopItem;
            _workshopItem.InstanceCountUpdated += WorkshopItemEntry_InstanceCountUpdated;
            _workshopItemShowInfoButton.SetCommand(CommandFactory.Instance.CreateShowWorkshopItemInfoCommand(_workshopItem));
            _workshopItemUnsubscribeButton.SetCommand(CommandFactory.Instance.CreateUnsubscribeWorkshopItemCommand(_workshopItem));

            SetValuesToUI();

            color = isOdd ? UIConstants.WorkshopItemRowOddColor : UIConstants.WorkshopItemRowEvenColor;
            isVisible = true;
        }

        /// <summary>
        /// Unloads the WorkshopItem from the workshopitemrow
        /// </summary>
        public void Unload()
        {
            if (_workshopItem != null)
                _workshopItem.InstanceCountUpdated -= WorkshopItemEntry_InstanceCountUpdated;
            _workshopItem = null;
            isVisible = false;
        }

        private UISprite CreateCellIcon(int xOffset, int yOffset)
        {
            var result = AddUIComponent<UISprite>();
            result.relativePosition = new Vector3(xOffset, yOffset);
            result.size = new Vector2(UIConstants.WorkshopItemTypeIconSize, UIConstants.WorkshopItemTypeIconSize);
            return result;
        }

        private UILabel CreateCellLabel(int xOffset, int yOffset, string labelText)
        {
            var result = AddUIComponent<UILabel>();
            result.relativePosition = new Vector3(xOffset, yOffset);
            result.textColor = UIConstants.WorkshopItemRowTextColor;
            result.textScale = UIConstants.WorkshopItemRowTextScale;
            result.text = labelText;
            return result;
        }

        private UICustomButton CreateInfoCellButton()
        {
            var result = AddUIComponent<UICustomButton>();
            result.size = new Vector2(UIConstants.WorkshopItemInfoButtonSize, UIConstants.WorkshopItemInfoButtonSize);
            result.relativePosition = new Vector3(UIConstants.WorkshopItemInfoButtonXOffset, UIConstants.RowButtonFieldYOffset);
            result.tooltip = UITexts.WorkshopItemInfoButtonToolTip;
            result.normalFgSprite = UIConstants.WorkshopItemInfoButtonNormalSprite;
            result.pressedFgSprite = UIConstants.WorkshopItemInfoButtonPressedSprite;
            result.hoveredFgSprite = UIConstants.WorkshopItemInfoButtonHoveredSprite;
            result.isVisible = true;

            return result;
        }

        private UICustomButton CreateUnsubscribeCellButton()
        {
            var result = AddUIComponent<UICustomButton>();
            result.size = new Vector2(UIConstants.WorkshopItemUnsubscribeButtonSize, UIConstants.WorkshopItemUnsubscribeButtonSize);
            result.relativePosition = new Vector3(UIConstants.WorkshopItemUnsubscribeButtonXOffset, UIConstants.RowButtonFieldYOffset);
            result.tooltip = UITexts.WorkshopItemUnsubscribeButtonToolTip;
            result.normalFgSprite = UIConstants.WorkshopItemUnsubscribeButtonNormalSprite;
            result.pressedFgSprite = UIConstants.WorkshopItemUnsubscribeButtonPressedSprite;
            result.hoveredFgSprite = UIConstants.WorkshopItemUnsubscribeButtonHoveredSprite;
            result.isVisible = true;

            return result;
        }

        private void SetValuesToUI()
        {
            if (_workshopItemNameLabel.text != _workshopItem.ReadableName)
                _workshopItemNameLabel.text = _workshopItem.ReadableName;
            string instanceCount = _workshopItem.InstanceCount.ToString();
            if (_numberUseLabel.text != instanceCount)
                _numberUseLabel.text = instanceCount;
            if (_workshopItemUnsubscribeButton != null)
                _workshopItemUnsubscribeButton.isVisible = _workshopItem.InstanceCount <= 0;
            _workshopItemTypeIcon.spriteName = UIConstants.GetBuildingTypeSprite(_workshopItem.BuildingType);
            _workshopItemTypeIcon.color = UIConstants.GetBuildingTypeColor(_workshopItem.BuildingType);
        }

        private void WorkshopItemEntry_InstanceCountUpdated(object sender, EventArgs e)
        {
            SetValuesToUI();
        }
    }
}