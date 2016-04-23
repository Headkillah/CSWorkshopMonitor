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
    public class OverwatchPropMonitor : OverwatchBaseMonitor
    {
        public override void OnReleased()
        {
            // Mark the prop monitor as no longer running
            OverwatchControl.Instance.PropMonitorSpun = false;

            // Clear the overwatch prop data
            if (OverwatchPropContainer.Instance != null)
                OverwatchPropContainer.Instance.ClearCache();

            base.OnReleased();
        }

        public override void OnBeforeSimulationTick()
        {
            // Exit if the monitor was terminated because of an error occured when updating overwatch data
            if (Terminated) return;

            // Exit if the prop monitor has not been loaded yet by the overwatch loader (prevents issues when the loader is not started yet but the monitor is)
            if (!OverwatchControl.Instance.PropMonitorSpun)
            {
                MarkUninitialized();
                return;
            }

            // Exit if the prop monitor has not been initialized yet (initialization happens in OnUpdate)
            if (!Initialized) return;

            // Exit if no prop changes occured since the previous tick
            if (!Singleton<PropManager>.instance.m_propsUpdated) return;

            try
            {
                for (int i = 0; i < Singleton<PropManager>.instance.m_updatedProps.Length; i++)
                {
                    ulong updatedPropId = Singleton<PropManager>.instance.m_updatedProps[i];
                    if (updatedPropId != 0)
                    {
                        for (int j = 0; j < 64; j++)
                        {
                            if ((updatedPropId & (ulong)1 << j) != 0)
                            {
                                ushort id = (ushort)(i << 6 | j);
                                ProcessProp(id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while updating prop information");
                ModLogger.Exception(ex);
                MarkTerminated();
            }

            base.OnBeforeSimulationTick();
        }

        protected override void InitializeMonitor()
        {
            ModLogger.Debug("Initializing prop monitor");

            try
            {
                // Clear any existing data from the overwatch container
                OverwatchPropContainer.Instance.ClearCache();

                // Process the list of existing props when initializing to make sure the list is up-to-date
                var capacity = Singleton<PropManager>.instance.m_props.m_buffer.Count();
                Enumerable.Range(0, capacity).DoAll(i => ProcessProp((ushort)i));

                // Store a reference to the current frame index so we know from which frame we need to process on the next update cycle
                MarkFrame();

                // Mark the monitor as initialized and spinning
                MarkInitialized();
                OverwatchControl.Instance.PropMonitorSpun = true;

                ModLogger.Debug("Prop monitor initialized");
            }
            catch (Exception ex)
            {
                ModLogger.Error("An error occured while initializing the prop monitor");
                ModLogger.Exception(ex);
                MarkTerminated();
            }
        }
        
        protected override void CheckAssetExistence(ushort assetId)
        {
            PropInstance propInstance;
            if (!TryGetProp(assetId, out propInstance))
                OverwatchPropContainer.Instance.RemoveAsset(assetId);
        }

        private bool TryGetProp(ushort propInstanceId, out PropInstance propInstance)
        {
            bool result = false;
            propInstance = default(PropInstance);

            var tryProp = Singleton<PropManager>.instance.m_props.m_buffer[(int)propInstanceId];

            if (tryProp.Info != null &&
               ((PropInstance.Flags)tryProp.m_flags & PropInstance.Flags.Created) != PropInstance.Flags.None)
            {
                propInstance = tryProp;
                result = true;
            }

            return result;
        }

        private bool ProcessProp(ushort propInstanceId)
        {
            if (OverwatchPropContainer.Instance.HasAsset(propInstanceId))
                OverwatchPropContainer.Instance.RemoveAsset(propInstanceId);

            PropInstance propInstance;
            if (!TryGetProp(propInstanceId, out propInstance))
                return false;

            OverwatchPropContainer.Instance.CacheAsset(propInstanceId, propInstance);

            return true;
        }
    }
}
