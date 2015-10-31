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

namespace CSAssetUsage
{
    public class OverwatchBuildingMonitor : ThreadingExtensionBase
    {
        private bool _initialized;
        private bool _terminated;
        private bool _paused;
        private int _lastProcessedFrame;

        private Building _currentBuilding;
        private ushort _currentId;

        private HashSet<ushort> _added;
        private HashSet<ushort> _removed;

        public override void OnCreated(IThreading threading)
        {
            _initialized = false;
            _terminated = false;

            _added = new HashSet<ushort>();
            _removed = new HashSet<ushort>();

            base.OnCreated(threading);
        }

        /*
         * Handles creation of new buildings and reallocation of existing buildings.
         *
         * Note: This needs to happen before simulation TICK; otherwise, we might miss the
         * building update tracking. The building update record gets cleared whether the
         * simulation is paused or not.
         */
        public override void OnBeforeSimulationTick()
        {
            if (_terminated) return;

            if (!OverwatchControl.Instance.BuildingMonitorSpun)
            {
                _initialized = false;
                return;
            }
            
            if (!_initialized) return;

            if (!Singleton<BuildingManager>.instance.m_buildingsUpdated) return;

            for (int i = 0; i < Singleton<BuildingManager>.instance.m_updatedBuildings.Length; i++)
            {
                ulong ub = Singleton<BuildingManager>.instance.m_updatedBuildings[i];

                if (ub != 0)
                {
                    for (int j = 0; j < 64; j++)
                    {
                        if ((ub & (ulong)1 << j) != 0)
                        {
                            ushort id = (ushort)(i << 6 | j);

                            if (ProcessBuilding(id))
                                _added.Add(id);
                            else
                                _removed.Add(id);
                        }
                    }
                }
            }

            base.OnBeforeSimulationTick();
        }

        public override void OnBeforeSimulationFrame()
        {
            base.OnBeforeSimulationFrame();
        }

        public override void OnAfterSimulationFrame()
        {
            _paused = false;

            base.OnAfterSimulationFrame();
        }

        public override void OnAfterSimulationTick()
        {
            base.OnAfterSimulationTick();
        }

        /*
         * Handles removal of buildings and status changes
         *
         * Note: Just because a building has been removed visually, it does not mean
         * it is removed as far as the game is concerned. The building is only truly removed
         * when the frame covers the building's id, and that's when we will remove the
         * building from our records.
         */
        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            if (_terminated) return;

            if (!OverwatchControl.Instance.BuildingMonitorSpinnable) return;

            try
            {
                if (!_initialized)
                {
                    _paused = false;

                    int capacity = (ushort)Singleton<BuildingManager>.instance.m_buildings.m_buffer.Length;

                    _currentId = (ushort)capacity;

                    _added.Clear();
                    _removed.Clear();

                    for (ushort i = 0; i < capacity; i++)
                    {
                        ProcessBuilding(i);
                    }

                    _lastProcessedFrame = GetFrame();

                    _initialized = true;
                    OverwatchControl.Instance.BuildingMonitorSpun = true;

                    ModLogger.Info("Building monitor initialized");
                }
                else if (!SimulationManager.instance.SimulationPaused)
                {
                    OverwatchData.Instance.ClearTrackerSets();
                    OverwatchData.Instance.LoadAndClearAdded(_added);
                    OverwatchData.Instance.LoadAndClearRemoved(_removed);

                    int end = GetFrame();

                    while (_lastProcessedFrame != end)
                    {
                        _lastProcessedFrame = GetFrame(_lastProcessedFrame + 1);

                        int[] boundaries = GetFrameBoundaries(_lastProcessedFrame);
                        ushort id;

                        for (int i = boundaries[0]; i <= boundaries[1]; i++)
                        {
                            id = (ushort)i;

                            if (UpdateBuilding(id))
                                OverwatchData.Instance.LoadUpdated(id);
                            else if (OverwatchData.Instance.HasBuilding(id))
                            {
                                OverwatchData.Instance.LoadRemoved(id);
                                OverwatchData.Instance.RemoveBuilding(id);
                            }
                        }
                    }
                }

                OutputDebugLog();
            }
            catch (Exception e)
            {
                string error = "Building monitor failed to initialize\r\n";
                error += String.Format("Error: {0}\r\n", e.Message);
                error += "\r\n";
                error += "==== STACK TRACE ====\r\n";
                error += e.StackTrace;

                ModLogger.Error(error);

                _terminated = true;
            }

            base.OnUpdate(realTimeDelta, simulationTimeDelta);
        }

        public override void OnReleased()
        {
            _initialized = false;
            _terminated = false;
            _paused = false;

            OverwatchControl.Instance.BuildingMonitorSpun = false;

            if (OverwatchData.Instance != null)
                OverwatchData.Instance.ClearAll();

            base.OnReleased();
        }

        public int GetFrameFromId(ushort id)
        {
            return id >> 7 & 255;
        }

        public int GetFrame()
        {
            return GetFrame((int)Singleton<SimulationManager>.instance.m_currentFrameIndex);
        }

        private int GetFrame(int index)
        {
            return (int)(index & 255);
        }

        private int[] GetFrameBoundaries()
        {
            return GetFrameBoundaries((int)Singleton<SimulationManager>.instance.m_currentFrameIndex);
        }

        private int[] GetFrameBoundaries(int index)
        {
            int frame = (int)(index & 255);
            int frame_first = frame * 128;
            int frame_last = (frame + 1) * 128 - 1;

            return new int[2] { frame_first, frame_last };
        }

        private bool GetBuilding()
        {
            _currentBuilding = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)_currentId];

            if (_currentBuilding.Info == null)
                return false;

            if ((_currentBuilding.m_flags & Building.Flags.Created) == Building.Flags.None)
                return false;

            return true;
        }

        private bool ProcessBuilding(ushort id)
        {
            if (OverwatchData.Instance.HasBuilding(id))
                OverwatchData.Instance.RemoveBuilding(id);

            _currentId = id;

            if (!GetBuilding())
                return false;

            if (!OverwatchData.Instance.CategorizeBuilding(_currentId, _currentBuilding))
            {
                ModLogger.Debug("No categories found for building {0}", id);
                return false;
            }

            return true;
        }

        private bool UpdateBuilding(ushort id)
        {
            _currentId = id;

            if (!GetBuilding())
                return false;

            return true;
        }

        internal void RequestRemoval(ushort id)
        {
            _currentId = id;

            if (!GetBuilding())
                OverwatchData.Instance.RemoveBuilding(id);
        }
        
        private bool Check(Building.Flags problems, HashSet<ushort> category)
        {
            if ((_currentBuilding.m_flags & problems) != Building.Flags.None)
            {
                category.Add(_currentId);
                return true;
            }
            else
            {
                category.Remove(_currentId);
                return false;
            }
        }

        private bool Check(Notification.Problem problems, HashSet<ushort> category)
        {
            if ((_currentBuilding.m_problems & problems) != Notification.Problem.None)
            {
                category.Add(_currentId);
                return true;
            }
            else
            {
                category.Remove(_currentId);
                return false;
            }
        }

        private void OutputDebugLog()
        {
            if (!OverwatchControl.Instance.BuildingMonitorSpun) return;
            if (!_initialized) return;
            if (!SimulationManager.instance.SimulationPaused) return;
            if (_paused) return;
            
            ModLogger.Debug(OverwatchData.Instance.GetLogString());

            _paused = true;
        }
    }
}
