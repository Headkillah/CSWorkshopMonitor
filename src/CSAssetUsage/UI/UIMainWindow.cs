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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace CSAssetUsage
{
    public delegate void SortAssetsDelegate(SortableAssetEntryField sortField = SortableAssetEntryField.Name);

    public class UIMainWindow : UIPanel
    {
        private UIPanel _mainPanel;
        private UIScrollablePanel _scrollablePanel;

        private UITitlePanel _titlePanel;
        //private UIButtonPanel _buttonPanel;
        private UICaptionPanel _captionPanel;

        private List<GameObject> _assetObjects;

        private UIAssetSorter _assetSorter;

        public override void Start()
        {
            ModLogger.Debug("Starting asset usage window");

            base.Start();

            _assetObjects = new List<GameObject>();
            _assetSorter = new UIAssetSorter();

            // Make the window invisible by default
            Hide();

            // Set the fixed window size
            width = UIConstants.MainWindowWidth;
            height = UIConstants.MainWindowHeight;

            // Define the window look
            backgroundSprite = UIConstants.MainWindowBackgroundSprite;
            UITextureAtlas atlas = Resources.FindObjectsOfTypeAll<UITextureAtlas>().FirstOrDefault(a => a.name == "Ingame");
            if (atlas != null)
                this.atlas = atlas;

            // Make the window interactive
            canFocus = true;
            isInteractive = true;

            // Set the layout settings
            autoLayout = true;
            autoLayoutDirection = LayoutDirection.Vertical;
            autoLayoutPadding = UIConstants.AutoLayoutPadding;
            autoLayoutStart = LayoutStart.TopLeft;

            // Create the window panels, scrollpanel and assetrows
            SetupPanels();
            SetupScrollPanel();
            SetupAssetRows();

            ModLogger.Debug("Asset usage window started");
        }

        public override void Update()
        {
            base.Update();

            if (isActivationKeyUsed())
            {
                ModLogger.Debug("Showing asset usage window");

                AssetMonitor.Instance.Update();
                CenterToParent();
                Show(true);
                Focus();

                ModLogger.Debug("Asset usage window showed");
            }
        }

        /// <summary>
        /// Called when a key is pressed.
        /// </summary>
        /// <param name="p">The event parameter.</param>
        protected override void OnKeyDown(UIKeyEventParameter p)
        {
            if (!p.used && p.keycode == KeyCode.Escape)
            {
                ModLogger.Debug("Hiding asset usage window");
                this.Hide();
                if (this.parent != null)
                    this.parent.Focus();
                p.Use();
                ModLogger.Debug("Asset usage window hidden");
            }
            base.OnKeyDown(p);
        }

        private void SetupPanels()
        {
            ModLogger.Debug("Setting up panels");

            // Create and add the title panel
            _titlePanel = AddUIComponent<UITitlePanel>();
            _titlePanel.Parent = this;

            // Create and add the caption panel
            _captionPanel = AddUIComponent<UICaptionPanel>();
            _captionPanel.SortDelegate = SortAssetsMethod;


            ModLogger.Debug("Panels set up");
        }

        private void SetupScrollPanel()
        {
            ModLogger.Debug("Setting up scroll panel");

            // Create the main panel hosting the scrollable panel
            _mainPanel = AddUIComponent<UIPanel>();
            _mainPanel.gameObject.AddComponent<UICustomControl>();
            _mainPanel.width = width - UIConstants.MainWindowMainPanelWidthOffset;
            _mainPanel.height = height - UIConstants.TitlePanelHeight - UIConstants.CaptionPanelLabelOffset - UIConstants.CaptionPanelHeight - autoLayoutPadding.bottom * 4 - autoLayoutPadding.top * 4;

            // taken from http://www.reddit.com/r/CitiesSkylinesModding/comments/2zrz0k/extended_public_transport_ui_provides_addtional/cpnet5q
            _scrollablePanel = _mainPanel.AddUIComponent<UIScrollablePanel>();
            _scrollablePanel.width = _scrollablePanel.parent.width - UIConstants.MainWindowScrollablePanelWidthOffset - 10;
            _scrollablePanel.height = _scrollablePanel.parent.height;
            _scrollablePanel.autoLayout = true;
            _scrollablePanel.autoLayoutDirection = LayoutDirection.Vertical;
            _scrollablePanel.autoLayoutStart = LayoutStart.TopLeft;
            _scrollablePanel.autoLayoutPadding = UIConstants.AutoLayoutPadding;
            _scrollablePanel.clipChildren = true;
            _scrollablePanel.pivot = UIPivotPoint.TopLeft;
            _scrollablePanel.AlignTo(_scrollablePanel.parent, UIAlignAnchor.TopLeft);
            _scrollablePanel.relativePosition = new Vector3(5, 0);

            UIScrollbar scrollbar = _mainPanel.AddUIComponent<UIScrollbar>();
            scrollbar.width = scrollbar.parent.width - _scrollablePanel.width;
            scrollbar.height = scrollbar.parent.height;
            scrollbar.orientation = UIOrientation.Vertical;
            scrollbar.pivot = UIPivotPoint.BottomLeft;
            scrollbar.AlignTo(scrollbar.parent, UIAlignAnchor.TopRight);
            scrollbar.minValue = 0;
            scrollbar.value = 0;
            scrollbar.incrementAmount = UIConstants.ScrollbarIncrementCount;

            UISlicedSprite trackSprite = scrollbar.AddUIComponent<UISlicedSprite>();
            trackSprite.relativePosition = Vector2.zero;
            trackSprite.autoSize = true;
            trackSprite.size = trackSprite.parent.size;
            trackSprite.fillDirection = UIFillDirection.Vertical;
            trackSprite.spriteName = UIConstants.ScrollbarTrackSprite;

            scrollbar.trackObject = trackSprite;

            UISlicedSprite thumbSprite = trackSprite.AddUIComponent<UISlicedSprite>();
            thumbSprite.relativePosition = Vector2.zero;
            thumbSprite.fillDirection = UIFillDirection.Vertical;
            thumbSprite.autoSize = true;
            thumbSprite.width = thumbSprite.parent.width;
            thumbSprite.spriteName = UIConstants.ScrollbarThumbSprite;

            scrollbar.thumbObject = thumbSprite;

            _scrollablePanel.verticalScrollbar = scrollbar;
            _scrollablePanel.eventMouseWheel += (component, param) =>
            {
                var sign = Math.Sign(param.wheelDelta);
                _scrollablePanel.scrollPosition += new Vector2(0, sign * (-1) * UIConstants.ScrollbarMouseWheelOffset);
            };

            ModLogger.Debug("Scroll panel set up");
        }

        private void SetupAssetRows()
        {
            var assetCount = AssetMonitor.Instance.GetAssetCount();
            ModLogger.Debug("{0} assets found", assetCount);

            bool odd = false;
            Enumerable.Range(0, assetCount).ForEach(i =>
            {
                //ModLogger.Debug(asset.NumberUsed.ToString());
                var assetObject = new GameObject("Asset");
                var assetRow = assetObject.AddComponent<UIAssetRow>();
                assetRow.IsOdd = odd;
                odd = !odd;
                _scrollablePanel.AttachUIComponent(assetObject);
                _assetObjects.Add(assetObject);
            });

            PopulateAssets();
        }

        private void PopulateAssets(SortableAssetEntryField sortField = SortableAssetEntryField.Name)
        {
            var assets = AssetMonitor.Instance.GetAssetList();
            _assetSorter.Sort(assets, sortField);
            Enumerable.Range(0, assets.Count).ForEach(i => _assetObjects[i].GetComponent<UIAssetRow>().Load(assets[i]));
        }

        private void ClearAssets()
        {
            _assetObjects.ForEach(ao => ao.GetComponent<UIAssetRow>().Unload());
        }

        private void SortAssetsMethod(SortableAssetEntryField sortField = SortableAssetEntryField.Name)
        {
            ClearAssets();
            PopulateAssets(sortField);
        }

        private bool isActivationKeyUsed()
        {
            // For now hardcoded activation key combination (ctrl+shift+A)
            return
                Input.GetKey(KeyCode.LeftControl) &&
                Input.GetKey(KeyCode.LeftShift) &&
                Input.GetKeyDown(KeyCode.A);
        }
    }
}
