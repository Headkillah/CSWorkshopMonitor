using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CSAssetUsage
{
    public class UIAssetSorter
    {
        private SortableAssetEntryField _currentSortField;
        private bool _descending;

        public UIAssetSorter()
        {
            _currentSortField = SortableAssetEntryField.Invalid;
        }

        public void Sort(List<AssetEntry> assets, SortableAssetEntryField sortField)
        {
            if (_currentSortField == sortField)
                _descending = !_descending;
            else
            {
                _descending = false;
                _currentSortField = sortField;
            }

            assets.Sort(new UIAssetComparer(sortField, _descending));
        }
    }
}
