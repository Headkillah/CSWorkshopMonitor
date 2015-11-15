/*
    The MIT License (MIT)

    Copyright (c) 2015 Aris Lancrescent

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

    https://github.com/arislancrescent/CS-SkylinesOverwatch
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Overwatch
{
    public class OverwatchContainer
    {
        private static readonly OverwatchContainer _instance = new OverwatchContainer();

        private Dictionary<ushort, OverwatchBuilding> _buildingCache;

        /// <summary>
        /// Prevents a default instance of the <see cref="OverwatchContainer"/> class from being created.
        /// </summary>
        private OverwatchContainer()
        {
            _buildingCache = new Dictionary<ushort, OverwatchBuilding>();
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="OverwatchContainer"/> class
        /// </summary>
        public static OverwatchContainer Instance
        {
            get { return _instance; }
        }

        public bool HasBuilding(ushort buildingId)
        {
            return _buildingCache.ContainsKey(buildingId);
        }

        public void CacheBuilding(ushort buildingId, Building building)
        {
            var overwatchBuilding = new OverwatchBuilding(buildingId, building);
            _buildingCache.Add(buildingId, overwatchBuilding);
        }

        public void ClearCache()
        {
            _buildingCache.Clear();
        }

        public void RemoveBuilding(ushort id)
        {
            if (_buildingCache.ContainsKey(id))
                _buildingCache.Remove(id);
        }

        public int GetBuildingCount(string technicalName)
        {
            return _buildingCache.Values.Count(b => b.TechincalName == technicalName);
        }
    }
}
