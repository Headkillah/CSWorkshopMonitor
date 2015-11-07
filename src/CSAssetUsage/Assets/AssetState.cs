using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    /// <summary>
    /// Represents a class holding the current sorting and filtering state of the list of assets
    /// </summary>
    public class AssetState
    {
        private IEnumerable<AssetEntry> _assetList;
        private BuildingType _currentFilter;
        private SortableAssetEntryField _currentSortField;
        private bool _descending;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetState"/> class.
        /// </summary>
        public AssetState()
        {
            _assetList = AssetMonitor.Instance.GetAssets();
            _currentFilter = BuildingType.All;
            _currentSortField = SortableAssetEntryField.Name;
        }

        /// <summary>
        /// Updates the filter state with a new filter
        /// </summary>
        /// <param name="filter">The filter.</param>
        public void SetFilter(BuildingType filter)
        {
            _currentFilter = filter;
        }

        /// <summary>
        /// Updates the sort state with a new sortfield
        /// </summary>
        /// <param name="sortField">The sort field.</param>
        public void SetSortField(SortableAssetEntryField sortField)
        {
            if (_currentSortField == sortField)
                // switch the sorting order when the new sort field is the same as the current one
                _descending = !_descending;
            else
            {
                // Reset the sorting order to ascending when the new sort field is different from the current one
                _descending = false;
                _currentSortField = sortField;
            }
        }

        /// <summary>
        /// Gets the list of assets with the current filtering and sort state
        /// </summary>
        /// <returns></returns>
        public List<AssetEntry> GetCurrentList()
        {
            try
            {
                ModLogger.Debug("Creating asset list with filter {0} and sortfield {1}", _currentFilter, _currentSortField);

                // Filter the current list and sort it
                var result = _assetList.Where(a => (a.BuildingType & _currentFilter) == a.BuildingType).ToList();
                result.Sort(new UIAssetComparer(_currentSortField, _descending));

                ModLogger.Debug("Created asset list contains {0} assets after filter", result.Count());

                return result;
            }
            catch (Exception ex)
            {
                ModLogger.Debug("An unexpected error occured while creating a filtered and sorted list of assets");
                ModLogger.Exception(ex);
                return new List<AssetEntry>();
            }
        }
    }
}
