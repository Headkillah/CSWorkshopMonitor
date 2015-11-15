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
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace WorkshopMonitor
{
    /// <summary>
    /// Represents a custom comparer for comparing workshop items, used to sort a list of workshop items on custom fields
    /// </summary>
    public class WorkshopItemComparer : Comparer<WorkshopItem>
    {
        private SortableWorkshopItemField _sortField;
        private bool _descending;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkshopItemComparer"/> class.
        /// </summary>
        /// <param name="sortField">The sort field.</param>
        /// <param name="descending">if set to <c>true</c> [descending].</param>
        public WorkshopItemComparer(SortableWorkshopItemField sortField, bool descending) : base()
        {
            _sortField = sortField;
            _descending = descending;
        }

        /// <summary>
        /// Compares two instances of the WorkshopItem class
        /// </summary>
        /// <param name="x">The first workshop item</param>
        /// <param name="y">The second workshop item</param>
        /// <returns></returns>
        public override int Compare(WorkshopItem x, WorkshopItem y)
        {
            object xPropertyValue = null;
            object yPropertyValue = null;

            switch (_sortField)
            {
                case SortableWorkshopItemField.ItemType:
                    xPropertyValue = (int)x.BuildingType;
                    yPropertyValue = (int)y.BuildingType;
                    break;
                case SortableWorkshopItemField.InstanceCount:
                    xPropertyValue = x.InstanceCount;
                    yPropertyValue = y.InstanceCount;
                    break;
                case SortableWorkshopItemField.Name:
                    xPropertyValue = x.ReadableName;
                    yPropertyValue = y.ReadableName;
                    break;
                default:
                    break;
            }

            if (xPropertyValue == yPropertyValue)
                return 0;

            var xTokens = Regex.Split(xPropertyValue.ToString().Replace(" ", ""), "([0-9]+)");
            var yTokens = Regex.Split(yPropertyValue.ToString().Replace(" ", ""), "([0-9]+)");

            for (var i = 0; i < xTokens.Length && i < yTokens.Length; ++i)
            {
                if (xTokens[i] == yTokens[i])
                    continue;

                int xValue;
                if (!int.TryParse(xTokens[i], out xValue))
                    return InvertIfDescending(String.Compare(xTokens[i], yTokens[i], StringComparison.OrdinalIgnoreCase));

                int yValue;
                if (!int.TryParse(yTokens[i], out yValue))
                    return InvertIfDescending(String.Compare(xTokens[i], yTokens[i], StringComparison.OrdinalIgnoreCase));

                return InvertIfDescending(xValue.CompareTo(yValue));
            }

            if (xTokens.Length < yTokens.Length)
                return InvertIfDescending(-1);
            if (xTokens.Length > yTokens.Length)
                return InvertIfDescending(1);

            return 0;
        }

        /// <summary>
        /// Inverts the sort order if the comparer is in descending mode
        /// </summary>
        /// <param name="sortResult">The sort result.</param>
        /// <returns></returns>
        private int InvertIfDescending(int sortResult)
        {
            if (_descending)
                return sortResult * -1;
            else
                return sortResult;
        }

    }
}
