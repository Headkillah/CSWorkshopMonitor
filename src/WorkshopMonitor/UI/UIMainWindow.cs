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
using WorkshopMonitor.Workshop;

namespace WorkshopMonitor.UI
{
    public class UIMainWindow : UIPanel
    {
        private UIPanel _mainPanel;
        private UIScrollablePanel _scrollablePanel;

        private UITitlePanel _titlePanel;
        private UIBuildingTypeFilterPanel _filterPanel;
        private UICaptionPanel _captionPanel;

        private List<GameObject> _workshopItemObjects;

        private WorkshopItemListState _workshopItemListState;

        public override void Start()
        {
            ModLogger.Debug("Starting WorkshopMonitor window");

            base.Start();

            _workshopItemObjects = new List<GameObject>();
            _workshopItemListState = new WorkshopItemListState();

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

            // Create the window panels, scrollpanel and workshopitemrows
            SetupPanels();
            SetupScrollPanel();
            SetupWorkshopItemRows();

            // Populate the workshopitem with workshopitem data
            PopulateWorkshopItems();

            ModLogger.Debug("WorkshopMonitor window started");
        }

        public override void Update()
        {
            base.Update();

            if (isActivationKeyUsed())
            {
                ModLogger.Debug("Displaying WorkshopMonitor window");

                // Always update the workshop monitor before showing the main window to ensure that the latest statistics are loaded
                WorkshopItemMonitor.Instance.Update();

                showWindow();

                ModLogger.Debug("WorkshopMonitor window displayed");
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _filterPanel.FilterChanged -= filterPanel_FilterChanged;
            _captionPanel.Sort -= captionPanel_Sort;
        }

        protected override void OnKeyDown(UIKeyEventParameter p)
        {
            if (!p.used && p.keycode == KeyCode.Escape)
            {
                ModLogger.Debug("Hiding WorkshopMonitor window");
                this.Hide();
                if (this.parent != null)
                    this.parent.Focus();
                p.Use();
                ModLogger.Debug("WorkshopMonitor window hidden");
            }
            base.OnKeyDown(p);
        }

        private void SetupPanels()
        {
            ModLogger.Debug("Setting up panels");

            // Create and add the title panel
            _titlePanel = AddUIComponent<UITitlePanel>();
            _titlePanel.Parent = this;

            // Create and add the filter panel
            _filterPanel = AddUIComponent<UIBuildingTypeFilterPanel>();
            _filterPanel.FilterChanged += filterPanel_FilterChanged;

            // Create and add the caption panel
            _captionPanel = AddUIComponent<UICaptionPanel>();
            _captionPanel.Sort += captionPanel_Sort;

            ModLogger.Debug("Panels set up");
        }

        private void SetupScrollPanel()
        {
            ModLogger.Debug("Setting up scroll panel");

            // Create the main panel hosting the scrollable panel
            _mainPanel = AddUIComponent<UIPanel>();
            _mainPanel.gameObject.AddComponent<UICustomControl>();
            _mainPanel.width = width - UIConstants.MainWindowMainPanelWidthOffset;
            int[] offsettingItems = new int[] { UIConstants.TitlePanelHeight, UIConstants.CaptionPanelLabelOffset, UIConstants.CaptionPanelHeight, UIConstants.FilterPanelHeight, autoLayoutPadding.bottom * 4, autoLayoutPadding.top * 4 };
            _mainPanel.height = height - offsettingItems.Sum();

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

        private void SetupWorkshopItemRows()
        {
            var workshopItemCount = WorkshopItemMonitor.Instance.GetWorkshopItemCount();
            ModLogger.Debug("{0} workshop items found", workshopItemCount);

            Enumerable.Range(0, workshopItemCount).ForEach(i =>
            {
                var workshopItemObject = new GameObject("WorkshopItemObject");
                workshopItemObject.AddComponent<UIWorkshopItemRow>();
                _scrollablePanel.AttachUIComponent(workshopItemObject);
                _workshopItemObjects.Add(workshopItemObject);
            });
        }

        private void PopulateWorkshopItems()
        {
            var workshopItems = _workshopItemListState.GetCurrentList();
            Enumerable.Range(0, workshopItems.Count).ForEach(i => _workshopItemObjects[i].GetComponent<UIWorkshopItemRow>().Load(workshopItems[i], i / 2 == 0));
        }

        private void ClearWorkshopItems()
        {
            _workshopItemObjects.ForEach(ao => ao.GetComponent<UIWorkshopItemRow>().Unload());
        }

        private void showWindow()
        {
            CenterToParent();
            Show(true);
            Focus();
        }

        private bool isActivationKeyUsed()
        {
            // For now hardcoded activation key combination (ctrl+shift+A)
            return
                Input.GetKey(KeyCode.LeftControl) &&
                Input.GetKey(KeyCode.LeftShift) &&
                Input.GetKeyDown(KeyCode.A);
        }

        private void captionPanel_Sort(object sender, SortEventArgs e)
        {
            ModLogger.Debug("Sorting data on {0}", e.SortField);
            ClearWorkshopItems();
            _workshopItemListState.SetSortField(e.SortField);
            PopulateWorkshopItems();
            ModLogger.Debug("Sorted data on {0}", e.SortField);
        }

        private void filterPanel_FilterChanged(object sender, FilterChangedEventArgs e)
        {
            try
            {
                ModLogger.Debug("Changing filter to {0}", e.NewFilter);
                ClearWorkshopItems();
                _workshopItemListState.SetFilter(e.NewFilter);
                PopulateWorkshopItems();
                ModLogger.Debug("Changed filter to {0}", e.NewFilter);
            }
            catch (Exception ex)
            {
                ModLogger.Exception(ex);
                throw;
            }
        }
    }
}
