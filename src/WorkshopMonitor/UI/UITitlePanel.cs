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
using UnityEngine;

namespace WorkshopMonitor.UI
{
    public class UITitlePanel : UIPanel
    {
        private UISprite _icon;
        private UILabel _title;
        private UIButton _close;
        private UIDragHandle _drag;

        public UIPanel Parent { get; set; }
        public string IconSprite { get; set; }

        public string TitleText
        {
            get { return _title.text; }
            set { _title.text = value; }
        }

        public override void Awake()
        {
            base.Awake();

            _icon = AddUIComponent<UISprite>();
            _title = AddUIComponent<UILabel>();
            _close = AddUIComponent<UIButton>();
            _drag = AddUIComponent<UIDragHandle>();

            height = UIConstants.TitlePanelHeight;
            width = UIConstants.TitlePanelWidth;
            TitleText = UITexts.WindowTitle ;
            IconSprite = "";
        }

        public override void Start()
        {
            base.Start();

            if (Parent == null)
            {
                ModLogger.Error(String.Format("Parent not set in {0}", this.GetType().Name));
                return;
            }

            width = Parent.width;
            relativePosition = Vector3.zero;
            isVisible = true;
            canFocus = true;
            isInteractive = true;

            _drag.width = width - UIConstants.DragHandleWidthOffset;
            _drag.height = height;
            _drag.relativePosition = Vector3.zero;
            _drag.target = Parent;

            _icon.spriteName = IconSprite;
            _icon.relativePosition = UIConstants.TitleIconRelativePosition;

            _title.relativePosition = UIConstants.TitleRelativePosition;
            _title.text = TitleText;

            _close.relativePosition = new Vector3(width - UIConstants.CloseButtonRelativePositionOffset, 2);
            _close.normalBgSprite = UIConstants.CloseButtonNormalBgSprite;
            _close.hoveredBgSprite = UIConstants.CloseButtonHoveredBgSprite;
            _close.pressedBgSprite = UIConstants.CloseButtonPressedBgSprite; 
            _close.eventClick += (component, param) => Parent.Hide();
        }
    }
}
