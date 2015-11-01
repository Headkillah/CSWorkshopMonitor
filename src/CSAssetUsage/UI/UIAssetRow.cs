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

namespace CSAssetUsage
{
    public class UIAssetRow : UIPanel
    {
        private UILabel _assetNameLabel;
        private UILabel _numberUseLabel;
        private UIButton _assetInfoButton;

        private AssetEntry _assetEntry;

        public bool IsOdd { get; set; }

        public void Load(AssetEntry assetEntry)
        {

            _assetEntry = assetEntry;
            _assetEntry.InstanceCountUpdated += assetEntry_InstanceCountUpdated;
        }

        public override void Awake()
        {
            base.Awake();

            height = UIConstants.AssetRowHeight;
            width = UIConstants.AssetRowWidth;
        }

        public override void Start()
        {
            base.Start();

            _assetNameLabel = createCellLabel(UIConstants.AssetNameColumnPosition, UIConstants.TextFieldRowPosition, string.Empty);
            _numberUseLabel = createCellLabel(UIConstants.NumberUsedColumnPosition, UIConstants.TextFieldRowPosition, 0.ToString());
            _assetInfoButton = createCellButton(UIConstants.AssetInfoButtonPosition, UIConstants.ButtonFieldRowPosition);

            // zebra stripes background
            backgroundSprite = UIConstants.AssetRowBackgroundSprite;
            color = IsOdd ? UIConstants.AssetRowOddColor : UIConstants.AssetRowEvenColor;
        }

        public override void Update()
        {
            base.Update();

            if (string.IsNullOrEmpty( _assetNameLabel.text))
                _assetNameLabel.text = _assetEntry.Metadata.name;
        }

        public override void OnDestroy()
        {
            _assetInfoButton.eventClick -= assetInfoButton_eventClick;
            _assetEntry.InstanceCountUpdated -= assetEntry_InstanceCountUpdated;
            base.OnDestroy();
        }

        private UILabel createCellLabel(int columnPosition, int rowPosition, string labelText)
        {
            var result = AddUIComponent<UILabel>();
            result.relativePosition = new Vector3(columnPosition, rowPosition);
            result.textColor = UIConstants.AssetRowTextColor;
            result.textScale = UIConstants.AssetRowTextScale;
            result.text = labelText;
            return result;
        }

        private UIButton createCellButton(int columnPosition, int rowPosition)
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

        private void assetInfoButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            //string assetUrl = string.Format("http://steamcommunity.com/sharedfiles/filedetails/?id={0}", _assetMetadata.PackageId);
            ////ModLogger.Debug(_asset.PackageId);
            //Process.Start(assetUrl);
        }

        
        private void assetEntry_InstanceCountUpdated(object sender, EventArgs e)
        {
            _numberUseLabel.text = _assetEntry.InstanceCount.ToString();
        }
    }
}