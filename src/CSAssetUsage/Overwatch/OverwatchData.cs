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
    public class OverwatchData
    {
        private static readonly OverwatchData _instance = new OverwatchData();

        private HashSet<ushort> _buildingsAdded;
        private HashSet<ushort> _buildingsUpdated;
        private HashSet<ushort> _buildingsRemoved;
        private Dictionary<ushort, OverwatchBuilding> _buildingCache;

        private OverwatchData()
        {
            _buildingsAdded = new HashSet<ushort>();
            _buildingsUpdated = new HashSet<ushort>();
            _buildingsRemoved = new HashSet<ushort>();
            _buildingCache = new Dictionary<ushort, OverwatchBuilding>();
        }

        public static OverwatchData Instance
        {
            get { return _instance; }
        }

        public bool HasBuilding(ushort id)
        {
            return _buildingCache.ContainsKey(id);
        }

        public bool CategorizeBuilding(ushort buildingId, Building building)
        {
            BuildingAI ai = building.Info.m_buildingAI;

            BuildingType buildingType = BuildingType.None;

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

            _buildingCache.Add(buildingId, new OverwatchBuilding(buildingId, building, buildingType));

            return true;
        }

        internal void ClearAll()
        {
            _buildingCache.Clear();
        }

        public void ClearTrackerSets()
        {
            _buildingsAdded.Clear();
            _buildingsUpdated.Clear();
            _buildingsRemoved.Clear();
        }

        public void LoadAndClearAdded(HashSet<ushort> added)
        {
            foreach (ushort i in added)
            {
                _buildingsAdded.Add(i);
            }
            added.Clear();
        }

        internal void LoadAndClearRemoved(HashSet<ushort> removed)
        {
            foreach (ushort i in removed)
            {
                _buildingsRemoved.Add(i);
            }

            removed.Clear();
        }

        internal void LoadUpdated(ushort id)
        {
            _buildingsUpdated.Add(id);
        }

        internal void LoadRemoved(ushort id)
        {
            _buildingsRemoved.Add(id);
        }

        internal void RemoveBuilding(ushort id)
        {
            _buildingCache.Remove(id);
        }

        internal string GetLogString()
        {
            StringBuilder log = new StringBuilder();
            log.AppendLine();
            log.AppendLine("==== BUILDINGS ====");
            log.AppendLine();
            log.AppendLine(String.Format("{0}   Total", _buildingCache.Count));
            log.AppendLine(String.Format("{0}   Added", _buildingsAdded.Count));
            log.AppendLine(String.Format("{0}   Updated", _buildingsUpdated.Count));
            log.AppendLine(String.Format("{0}   Removed", _buildingsRemoved.Count));
            log.AppendLine();
            log.AppendLine(String.Format("{0}   Player Building(s)", GetBuildingCount(BuildingType.PlayerBuilding)));
            log.AppendLine(String.Format(" =>   {0}   Cemetery(s)", GetBuildingCount(BuildingType.Cemetery)));
            log.AppendLine(String.Format(" =>   {0}   LandfillSite(s)", GetBuildingCount(BuildingType.LandfillSite)));
            log.AppendLine(String.Format(" =>   {0}   FireStation(s)", GetBuildingCount(BuildingType.FireStation)));
            log.AppendLine(String.Format(" =>   {0}   PoliceStation(s)", GetBuildingCount(BuildingType.PoliceStation)));
            log.AppendLine(String.Format(" =>   {0}   Hospital(s)", GetBuildingCount(BuildingType.Hospital)));
            log.AppendLine(String.Format(" =>   {0}   Park(s)", GetBuildingCount(BuildingType.Park)));
            log.AppendLine(String.Format(" =>   {0}   PowerPlant(s)", GetBuildingCount(BuildingType.PowerPlant)));
            log.AppendLine(String.Format(" =>   {0}   WaterFacitlity(s)", GetBuildingCount(BuildingType.WaterFacility)));
            log.AppendLine(String.Format(" =>   {0}   Other", GetBuildingCount(BuildingType.PlayerOther)));
            log.AppendLine();
            log.AppendLine(String.Format("{0}   Zoned Building(s)", GetBuildingCount(BuildingType.ZonedBuilding)));
            log.AppendLine(String.Format(" =>   {0}   Residential", GetBuildingCount(BuildingType.ResidentialBuilding)));
            log.AppendLine(String.Format(" =>   {0}   Commercial", GetBuildingCount(BuildingType.CommercialBuilding)));
            log.AppendLine(String.Format(" =>   {0}   Industrial", GetBuildingCount(BuildingType.IndustrialBuilding)));
            log.AppendLine(String.Format(" =>   {0}   Office(s)", GetBuildingCount(BuildingType.OfficeBuilding)));
            log.AppendLine(String.Format(" =>   {0}   Other", GetBuildingCount(BuildingType.ZonedOther)));
            log.AppendLine();
            log.AppendLine(String.Format("{0}   Other Building(s)", GetBuildingCount(BuildingType.Other)));
            log.AppendLine();

            return log.ToString();
        }

        public int GetBuildingCount(BuildingType buildingType)
        {
            return _buildingCache.Values.Count(b => (b.Type & buildingType) == buildingType);
        }

        public int GetBuildingCount(string packageId)
        {
            return _buildingCache.Values.Count(b => b.Building.Info.name.StartsWith(packageId));
        }
    }
}
