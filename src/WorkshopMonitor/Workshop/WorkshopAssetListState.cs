using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Overwatch;
using WorkshopMonitor.UI;

namespace WorkshopMonitor.Workshop
{
    public class WorkshopAssetListState
    {
        private AssetType _currentFilter;
        private SortableWorkshopAssetField _currentSortField;
        private bool _descending;
        
        public WorkshopAssetListState(AssetType initialFilter)
        {
            _currentFilter = initialFilter;
            _currentSortField = SortableWorkshopAssetField.Name;
        }

        public void SetFilter(AssetType filter)
        {
            _currentFilter = filter;
        }

        public void SetSortField(SortableWorkshopAssetField sortField)
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

        public List<IUIWorkshopAssetRowData> GetCurrentList()
        {
            try
            {
                ModLogger.Debug("Creating workshop item list with filter {0} and sortfield {1}", _currentFilter, _currentSortField);

                // Filter the current list and sort it
                var result = WorkshopAssetMonitor.Instance.GetWorkshopAssets()
                    .Where(a => (a.AssetType & _currentFilter) == a.AssetType)
                    .Cast<IUIWorkshopAssetRowData>()
                    .ToList();

                result.Sort(new WorkshopAssetComparer(_currentSortField, _descending));

                ModLogger.Debug("Created workshop item list with {0} items after filter", result.Count());

                return result;
            }
            catch (Exception ex)
            {
                ModLogger.Debug("An unexpected error occured while creating a filtered and sorted list of workshop items");
                ModLogger.Exception(ex);
                return new List<IUIWorkshopAssetRowData>();
            }
        }
    }
}
