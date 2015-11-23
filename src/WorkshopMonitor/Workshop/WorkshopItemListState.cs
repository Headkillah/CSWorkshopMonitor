using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Overwatch;

namespace WorkshopMonitor.Workshop
{
    public class WorkshopItemListState
    {
        private BuildingType _currentFilter;
        private SortableWorkshopItemField _currentSortField;
        private bool _descending;

        public WorkshopItemListState()
        {
            _currentFilter = BuildingType.All;
            _currentSortField = SortableWorkshopItemField.Name;
        }

        public void SetFilter(BuildingType filter)
        {
            _currentFilter = filter;
        }

        public void SetSortField(SortableWorkshopItemField sortField)
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

        public List<WorkshopItem> GetCurrentList()
        {
            try
            {
                ModLogger.Debug("Creating workshop item list with filter {0} and sortfield {1}", _currentFilter, _currentSortField);

                // Filter the current list and sort it
                var result = WorkshopItemMonitor.Instance.GetWorkshopItems().Where(a => (a.BuildingType & _currentFilter) == a.BuildingType).ToList();
                result.Sort(new WorkshopItemComparer(_currentSortField, _descending));

                ModLogger.Debug("Created workshop item list with {0} items after filter", result.Count());

                return result;
            }
            catch (Exception ex)
            {
                ModLogger.Debug("An unexpected error occured while creating a filtered and sorted list of workshop items");
                ModLogger.Exception(ex);
                return new List<WorkshopItem>();
            }
        }
    }
}
