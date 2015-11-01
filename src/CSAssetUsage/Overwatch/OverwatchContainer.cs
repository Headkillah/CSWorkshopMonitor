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

namespace CSAssetUsage
{
    /// <summary>
    /// Represents a class responsible for holding the building information as collected by the building monitor
    /// </summary>
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

        /// <summary>
        /// Determines whether a building with the specified identifier exists in the data
        /// </summary>
        /// <param name="buildingId">The building identifier</param>
        /// <returns></returns>
        public bool HasBuilding(ushort buildingId)
        {
            return _buildingCache.ContainsKey(buildingId);
        }

        /// <summary>
        /// Categorizes and caches a building with a given id. The building is categorized based on its' AI and stored in the building cache for quick retrieval.
        /// If no category could be determined for the building it is NOT cached.
        /// </summary>
        /// <param name="buildingId">The building identifier</param>
        /// <param name="building">The building</param>
        /// <returns>True if the building could be categorized and cached, false otherwise</returns>
        public bool CategorizeBuilding(ushort buildingId, Building building)
        {
            BuildingType buildingType = BuildingType.None;

            // Check the AI of the building and determine the building type based on the AI
            BuildingAI ai = building.Info.m_buildingAI;
            if (ai is PlayerBuildingAI)
            {
                if (ai is CemeteryAI)
                    buildingType = BuildingType.Cemetery;
                else if (ai is LandfillSiteAI)
                    buildingType = BuildingType.LandfillSite;
                else if (ai is FireStationAI)
                    buildingType = BuildingType.FireStation;
                else if (ai is PoliceStationAI)
                    buildingType = BuildingType.PoliceStation;
                else if (ai is HospitalAI)
                    buildingType = BuildingType.Hospital;
                else if (ai is ParkAI)
                    buildingType = BuildingType.Park;
                else if (ai is PowerPlantAI)
                    buildingType = BuildingType.PowerPlant;
                else if (ai is WaterFacilityAI)
                    buildingType = BuildingType.WaterFacility;
                else
                    buildingType = BuildingType.PlayerOther;
            }
            else if (ai is PrivateBuildingAI)
            {
                if (ai is ResidentialBuildingAI)
                    buildingType = BuildingType.ResidentialBuilding;
                else if (ai is CommercialBuildingAI)
                    buildingType = BuildingType.CommercialBuilding;
                else if (ai is IndustrialBuildingAI)
                    buildingType = BuildingType.IndustrialBuilding;
                else if (ai is OfficeBuildingAI)
                    buildingType = BuildingType.OfficeBuilding;
                else
                    buildingType = BuildingType.ZonedOther;
            }
            else
                buildingType = BuildingType.Other;

            if (buildingType == BuildingType.None)
                return false;

            // Only add the building to the cache if it could be categorized
            CacheBuilding(buildingId, new OverwatchBuilding(buildingId, building, buildingType));
            
            return true;
        }

        /// <summary>
        /// Clears all buildings from the building cache
        /// </summary>
        public void ClearCache()
        {
            _buildingCache.Clear();
        }

        /// <summary>
        /// Removes a building with a given identifier from the building cache
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void RemoveBuilding(ushort id)
        {
            if (_buildingCache.ContainsKey(id))
                _buildingCache.Remove(id);
        }

        /// <summary>
        /// Gets the number of buildings which were build from a package with a given package identifier
        /// </summary>
        /// <param name="packageId">The package identifier.</param>
        /// <returns></returns>
        public int GetBuildingCount(string packageId)
        {
            return _buildingCache.Values.Count(b => b.SourcePackageId.Equals(packageId, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Adds a building to the building cache
        /// </summary>
        /// <param name="buildingId">The building identifier</param>
        /// <param name="building">The building</param>
        private void CacheBuilding(ushort buildingId, OverwatchBuilding building)
        {
            _buildingCache.Add(buildingId, building);
        }
    }
}
