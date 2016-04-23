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

using ColossalFramework;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Overwatch
{
    public class OverwatchBuildingMonitor : OverwatchBaseMonitor
    {
        public override void OnReleased()
        {
            // Mark the monitor as no longer running
            OverwatchControl.Instance.BuildingMonitorSpun = false;

            // Clear the overwatch data
            if (OverwatchBuildingContainer.Instance != null)
                OverwatchBuildingContainer.Instance.ClearCache();

            base.OnReleased();
        }

        public override void OnBeforeSimulationTick()
        {
            // Exit if the monitor was terminated because of an error occured when updating overwatch data
            if (Terminated) return;

            // Exit if the building monitor has not been loaded yet by the overwatch loader (prevents issues when the loader is not started yet but the monitor is)
            if (!OverwatchControl.Instance.BuildingMonitorSpun)
            {
                MarkUninitialized();
                return;
            }

            // Exit if the building monitor has not been initialized yet (initialization happens in OnUpdate)
            if (!Initialized) return;

            // Exit if no building changes occured since the previous tick
            if (!Singleton<BuildingManager>.instance.m_buildingsUpdated) return;
            
            try
            {
                for (int i = 0; i < Singleton<BuildingManager>.instance.m_updatedBuildings.Length; i++)
                {
                    ulong updatedBuildingId = Singleton<BuildingManager>.instance.m_updatedBuildings[i];
                    if (updatedBuildingId != 0)
                    {
                        for (int j = 0; j < 64; j++)
                        {
                            if ((updatedBuildingId & (ulong)1 << j) != 0)
                            {
                                ushort id = (ushort)(i << 6 | j);
                                ProcessBuilding(id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while updating building information");
                ModLogger.Exception(ex);
                MarkTerminated();
            }

            base.OnBeforeSimulationTick();
        }


        protected override void InitializeMonitor()
        {
            ModLogger.Debug("Initializing building monitor");

            try
            {
                // Clear any existing data from the overwatch container
                OverwatchBuildingContainer.Instance.ClearCache();
                
                // Process the list of existing buildings when initializing to make sure the list is up-to-date
                var capacity = (ushort)Singleton<BuildingManager>.instance.m_buildings.m_buffer.Length;
                Enumerable.Range(0, capacity).DoAll(i => ProcessBuilding((ushort)i));

                // Store a reference to the current frame index so we know from which frame we need to process on the next update cycle
                MarkFrame();

                // Mark the monitor as initialized and spinning
                MarkInitialized();
                OverwatchControl.Instance.BuildingMonitorSpun = true;

                ModLogger.Debug("Building monitor initialized");
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while initializing the building monitor");
                ModLogger.Exception(ex);
                MarkTerminated();
            }
        }

        protected override void CheckAssetExistence(ushort assetId)
        {
            Building building;
            if (!TryGetBuilding(assetId, out building))
                OverwatchBuildingContainer.Instance.RemoveAsset(assetId);
        }

        private bool TryGetBuilding(ushort buildingId, out Building building)
        {
            bool result = false;
            building = default(Building);

            var tryBuilding = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)buildingId];

            if (tryBuilding.Info != null &&
                (tryBuilding.m_flags & Building.Flags.Created) != Building.Flags.None)
            {
                building = tryBuilding;
                result = true;
            }

            return result;
        }

        private bool ProcessBuilding(ushort buildingId)
        {
            if (OverwatchBuildingContainer.Instance.HasAsset(buildingId))
                OverwatchBuildingContainer.Instance.RemoveAsset(buildingId);

            Building building;
            if (!TryGetBuilding(buildingId, out building))
                return false;

            OverwatchBuildingContainer.Instance.CacheAsset(buildingId, building);

            return true;
        }
    }
}
