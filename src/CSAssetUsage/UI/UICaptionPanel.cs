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

namespace CSAssetUsage
{
    public class UICaptionPanel : UIPanel
    {
        public event EventHandler<SortEventArgs> Sort;

        public override void Start()
        {
            base.Start();

            createLabel(UIConstants.AssetTypeLabelXOffset, UITexts.AssetTypeColumnLabel, SortableAssetEntryField.AssetType);
            createLabel(UIConstants.AssetNameLabelXOffset, UITexts.AssetNameColumnLabel, SortableAssetEntryField.Name);
            createLabel(UIConstants.NumberUsedLabelXOffset, UITexts.NumberUsedColumnLabel, SortableAssetEntryField.InstanceCount);
            
            width = UIConstants.CaptionPanelWidth;
            height = UIConstants.CaptionPanelHeight;
        }

        private UILabel createLabel(int columnPosition, string text, SortableAssetEntryField sortField)
        {
            var result = AddUIComponent<UILabel>();
            result.relativePosition = new Vector3(columnPosition, UIConstants.CaptionPanelLabelOffset);
            result.textScale = UIConstants.CaptionPanelTextScale;
            result.text = text;
            result.eventClick += (component, param) => OnSort(sortField);
            return result;
        }

        protected virtual void OnSort(SortableAssetEntryField sortField)
        {
            var handler = Sort;
            if (handler != null)
                handler.Invoke(this, new SortEventArgs(sortField));
        }
    }
}
