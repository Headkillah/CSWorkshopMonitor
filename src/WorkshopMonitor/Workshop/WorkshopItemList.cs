using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    /// <summary>
    /// Represents a class managing the current sorting and filtering state of the list of workshop items
    /// </summary>
    public class WorkshopItemListState
    {
        private IEnumerable<WorkshopItem> _workshopItemList;
        private BuildingType _currentFilter;
        private SortableWorkshopItemField _currentSortField;
        private bool _descending;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkshopItemListState"/> class.
        /// </summary>
        public WorkshopItemListState()
        {
            _workshopItemList = WorkshopItemMonitor.Instance.GetWorkshopItems();
            _currentFilter = BuildingType.All;
            _currentSortField = SortableWorkshopItemField.Name;
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

        /// <summary>
        /// Gets the list of workshop items with the current filtering and sort state
        /// </summary>
        /// <returns></returns>
        public List<WorkshopItem> GetCurrentList()
        {
            try
            {
                ModLogger.Debug("Creating workshop item list with filter {0} and sortfield {1}", _currentFilter, _currentSortField);

                // Filter the current list and sort it
                var result = _workshopItemList.Where(a => (a.BuildingType & _currentFilter) == a.BuildingType).ToList();
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
