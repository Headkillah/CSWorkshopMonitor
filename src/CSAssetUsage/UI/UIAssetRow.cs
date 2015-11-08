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

namespace CSAssetUsage
{
    /// <summary>
    /// Represents a uipanel class showing the information of a single asset in a 'row; style
    /// </summary>
    public class UIAssetRow : UIPanel
    {
        private UISprite _assetTypeIcon;
        private UILabel _assetNameLabel;
        private UILabel _numberUseLabel;
        private UIButton _assetInfoButton;

        private AssetEntry _assetEntry;
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is on an 'odd' or 'even' position. Used to create 'zebra' like coloring of the rows
        /// </summary>
        public bool IsOdd { get; set; }

        /// <summary>
        /// Invoked by the unity engine
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            height = UIConstants.AssetRowHeight;
            width = UIConstants.AssetRowWidth;
        }

        /// <summary>
        /// Invoked by the unity engine
        /// </summary>
        public override void Start()
        {
            base.Start();

            _assetTypeIcon = CreateCellIcon(UIConstants.AssetTypeIconXOffset, UIConstants.AssetTypeYOffset);
            _assetNameLabel = CreateCellLabel(UIConstants.AssetNameLabelXOffset, UIConstants.LabelYOffset, string.Empty);
            _numberUseLabel = CreateCellLabel(UIConstants.NumberUsedLabelXOffset, UIConstants.LabelYOffset, 0.ToString());
            _assetInfoButton = CreateCellButton(UIConstants.AssetInfoButtonXOffset, UIConstants.ButtonFieldYOffset);

            // zebra stripes background
            backgroundSprite = UIConstants.AssetRowBackgroundSprite;
            color = IsOdd ? UIConstants.AssetRowOddColor : UIConstants.AssetRowEvenColor;
        }

        private UISprite CreateCellIcon(object assetTypeIconXOffset, object assetTypeYOffset)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invoked by the unity engine
        /// </summary>
        public override void Update()
        {
            base.Update();

            SetValuesToUI();
        }

        /// <summary>
        /// Invoked by the unity engine
        /// </summary>
        public override void OnDestroy()
        {
            // Make sure eventhandlers are destroyed when the panel is destroyed by unity
            _assetInfoButton.eventClick -= assetInfoButton_eventClick;
            _assetEntry.InstanceCountUpdated -= assetEntry_InstanceCountUpdated;
            base.OnDestroy();
        }

        /// <summary>
        /// Loads the specified asset entry into the assetrow
        /// </summary>
        /// <param name="assetEntry">The asset entry.</param>
        public void Load(AssetEntry assetEntry)
        {
            _assetEntry = assetEntry;
            _assetEntry.InstanceCountUpdated += assetEntry_InstanceCountUpdated;
            isVisible = true;
        }

        /// <summary>
        /// Unloads the assetentry from the asset row
        /// </summary>
        public void Unload()
        {
            if (_assetEntry != null)
                _assetEntry.InstanceCountUpdated -= assetEntry_InstanceCountUpdated;
            _assetEntry = null;
            isVisible = false;
        }

        private UISprite CreateCellIcon(int xOffset, int yOffset)
        {
            var result = AddUIComponent<UISprite>();
            result.relativePosition = new Vector3(xOffset, yOffset);
            result.size = new Vector2(UIConstants.AssetTypeIconSize, UIConstants.AssetTypeIconSize);
            return result;
        }

        private UILabel CreateCellLabel(int xOffset, int yOffset, string labelText)
        {
            var result = AddUIComponent<UILabel>();
            result.relativePosition = new Vector3(xOffset, yOffset);
            result.textColor = UIConstants.AssetRowTextColor;
            result.textScale = UIConstants.AssetRowTextScale;
            result.text = labelText;
            return result;
        }

        private UIButton CreateCellButton(int columnPosition, int rowPosition)
        {
            var result = AddUIComponent<UIButton>();
            result.size = new Vector2(UIConstants.AssetInfoButtonSize, UIConstants.AssetInfoButtonSize);
            result.relativePosition = new Vector3(columnPosition, rowPosition);
            result.tooltip = UITexts.AssetInfoButtonToolTip;
            result.normalFgSprite = UIConstants.AssetInfoButtonNormalSprite;
            result.pressedFgSprite = UIConstants.AssetInfoButtonPressedSprite;
            result.hoveredFgSprite = UIConstants.AssetInfoButtonHoveredSprite;
            result.isVisible = true;
            result.eventClick += assetInfoButton_eventClick;
            return result;
        }
        
        private void ShowModalCallback(UIComponent component, int result)
        {
            if (result != 0)
            {
                string assetUrl = string.Format("http://steamcommunity.com/sharedfiles/filedetails/?id={0}", _assetEntry.PackageId);
                Process.Start(assetUrl);
            }
        }

        private void SetValuesToUI()
        {
            if (_assetNameLabel.text != _assetEntry.Metadata.name)
                _assetNameLabel.text = _assetEntry.Metadata.name;
            string instanceCount = _assetEntry.InstanceCount.ToString();
            if (_numberUseLabel.text != instanceCount)
                _numberUseLabel.text = instanceCount;
            _assetTypeIcon.spriteName = UIConstants.GetBuildingTypeSprite(_assetEntry.BuildingType);
            _assetTypeIcon.color = UIConstants.GetBuildingTypeColor(_assetEntry.BuildingType);
        }

        private void assetInfoButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            ulong packageId = 0;
            if (Steam.IsOverlayEnabled() && ulong.TryParse(_assetEntry.PackageId, out packageId))
                Steam.ActivateGameOverlayToWorkshopItem(new PublishedFileId(packageId));
            else
                ConfirmPanel.ShowModal(UITexts.AssetInfoOpenInBrowserTitle, UITexts.AssetInfoOpenInBrowserMessage, ShowModalCallback);
        }

        private void assetEntry_InstanceCountUpdated(object sender, EventArgs e)
        {
            SetValuesToUI();
        }
    }
}