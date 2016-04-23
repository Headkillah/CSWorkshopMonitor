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
    public class UIWorkshopAssetRow : UIPanel
    {
        private UISprite _workshopAssetTypeIcon;
        private UILabel _workshopIdLabel;
        private UILabel _workshopAssetNameLabel;
        private UILabel _numberUseLabel;
        private UICustomButton _workshopAssetShowInfoButton;
        private UICustomButton _workshopAssetUnsubscribeButton;

        private IUIWorkshopAssetRowData _workshopAssetRowData;

       

        public ulong WorkshopId
        {
            get
            {
                ulong result = 0;
                if (_workshopAssetRowData != null)
                    result = _workshopAssetRowData.WorkshopId;
                return result;
            }
        }

        public override void Awake()
        {
            base.Awake();

            height = UIConstants.WorkshopAssetRowHeight;
            width = UIConstants.WorkshopAssetRowWidth;

            _workshopAssetTypeIcon = CreateCellIcon(UIConstants.WorkshopAssetTypeIconXOffset, UIConstants.WorkshopAssetTypeYOffset);
            _workshopIdLabel = CreateCellLabel(UIConstants.WorkshopAssetWorkshopIdLabelXOffset, UIConstants.LabelYOffset, string.Empty);
            _workshopAssetNameLabel = CreateCellLabel(UIConstants.WorkshopAssetNameLabelXOffset, UIConstants.LabelYOffset, string.Empty);
            _numberUseLabel = CreateCellLabel(UIConstants.NumberUsedLabelXOffset, UIConstants.LabelYOffset, 0.ToString());
            _workshopAssetShowInfoButton = CreateInfoCellButton();
            _workshopAssetUnsubscribeButton = CreateUnsubscribeCellButton();

            // zebra stripes background
            backgroundSprite = UIConstants.WorkshopAssetRowBackgroundSprite;
        }

        /// <summary>
        /// Invoked by the unity engine
        /// </summary>
        public override void OnDestroy()
        {
            // Make sure eventhandlers are destroyed when the panel is destroyed by unity
            if (_workshopAssetRowData != null)
                _workshopAssetRowData.InstanceCountUpdated -= WorkshopAssetEntry_InstanceCountUpdated;
            base.OnDestroy();
        }

        /// <summary>
        /// Loads the specified workshop asset into the workshopassetrow
        /// </summary>
        /// <param name="workshopAssetRowData">The workshop asset entry.</param>
        public void Load(IUIWorkshopAssetRowData workshopAssetRowData, bool isOdd)
        {
            _workshopAssetRowData = workshopAssetRowData;
            _workshopAssetRowData.InstanceCountUpdated += WorkshopAssetEntry_InstanceCountUpdated;
            _workshopAssetShowInfoButton.SetCommand(CommandFactory.Instance.CreateShowWorkshopAssetInfoCommand(_workshopAssetRowData));
            _workshopAssetUnsubscribeButton.SetCommand(CommandFactory.Instance.CreateUnsubscribeWorkshopAssetCommand(_workshopAssetRowData));

            SetValuesToUI();

            color = isOdd ? UIConstants.WorkshopAssetRowOddColor : UIConstants.WorkshopAssetRowEvenColor;
            isVisible = true;
        }

        /// <summary>
        /// Unloads the WorkshopAsset from the workshopassetrow
        /// </summary>
        public void Unload()
        {
            if (_workshopAssetRowData != null)
                _workshopAssetRowData.InstanceCountUpdated -= WorkshopAssetEntry_InstanceCountUpdated;
            _workshopAssetRowData = null;
            isVisible = false;
        }

        private UISprite CreateCellIcon(int xOffset, int yOffset)
        {
            var result = AddUIComponent<UISprite>();
            result.relativePosition = new Vector3(xOffset, yOffset);
            result.size = new Vector2(UIConstants.WorkshopAssetTypeIconSize, UIConstants.WorkshopAssetTypeIconSize);
            return result;
        }

        private UILabel CreateCellLabel(int xOffset, int yOffset, string labelText)
        {
            var result = AddUIComponent<UILabel>();
            result.relativePosition = new Vector3(xOffset, yOffset);
            result.textColor = UIConstants.WorkshopAssetRowTextColor;
            result.textScale = UIConstants.WorkshopAssetRowTextScale;
            result.text = labelText;
            return result;
        }

        private UICustomButton CreateInfoCellButton()
        {
            var result = AddUIComponent<UICustomButton>();
            result.size = new Vector2(UIConstants.WorkshopAssetInfoButtonSize, UIConstants.WorkshopAssetInfoButtonSize);
            result.relativePosition = new Vector3(UIConstants.WorkshopAssetInfoButtonXOffset, UIConstants.RowButtonFieldYOffset);
            result.tooltip = UITexts.WorkshopAssetInfoButtonToolTip;
            result.normalFgSprite = UIConstants.WorkshopAssetInfoButtonNormalSprite;
            result.pressedFgSprite = UIConstants.WorkshopAssetInfoButtonPressedSprite;
            result.hoveredFgSprite = UIConstants.WorkshopAssetInfoButtonHoveredSprite;
            result.isVisible = true;

            return result;
        }

        private UICustomButton CreateUnsubscribeCellButton()
        {
            var result = AddUIComponent<UICustomButton>();
            result.size = new Vector2(UIConstants.WorkshopAssetUnsubscribeButtonSize, UIConstants.WorkshopAssetUnsubscribeButtonSize);
            result.relativePosition = new Vector3(UIConstants.WorkshopAssetUnsubscribeButtonXOffset, UIConstants.RowButtonFieldYOffset);
            result.tooltip = UITexts.WorkshopAssetUnsubscribeButtonToolTip;
            result.normalFgSprite = UIConstants.WorkshopAssetUnsubscribeButtonNormalSprite;
            result.pressedFgSprite = UIConstants.WorkshopAssetUnsubscribeButtonPressedSprite;
            result.hoveredFgSprite = UIConstants.WorkshopAssetUnsubscribeButtonHoveredSprite;
            result.isVisible = true;

            return result;
        }

        private void SetValuesToUI()
        {
            if (_workshopAssetNameLabel.text != _workshopAssetRowData.ReadableName)
                _workshopAssetNameLabel.text = _workshopAssetRowData.ReadableName;
            if (_workshopIdLabel.text != _workshopAssetRowData.WorkshopId.ToString())
                _workshopIdLabel.text = _workshopAssetRowData.WorkshopId.ToString();
            string instanceCount = _workshopAssetRowData.InstanceCount.ToString();
            if (_numberUseLabel.text != instanceCount)
                _numberUseLabel.text = instanceCount;
            if (_workshopAssetUnsubscribeButton != null)
                _workshopAssetUnsubscribeButton.isVisible = _workshopAssetRowData.InstanceCount <= 0;
            _workshopAssetTypeIcon.spriteName = _workshopAssetRowData.SpriteName;
            _workshopAssetTypeIcon.color = _workshopAssetRowData.Color;
        }

        private void WorkshopAssetEntry_InstanceCountUpdated(object sender, EventArgs e)
        {
            SetValuesToUI();
        }
    }
}