using ColossalFramework;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Overwatch
{
    public abstract class OverwatchBaseMonitor : ThreadingExtensionBase
    {
        private bool _initialized;
        private bool _terminated;
        private int _lastProcessedFrame;

        private string _monitorType;

        protected abstract void InitializeMonitor();

        protected abstract void CheckAssetExistence(ushort assetId);

        public OverwatchBaseMonitor()
        {
            _monitorType = this.GetType().Name;
        }

        protected bool Initialized
        {
            get { return _initialized; }
        }

        protected bool Terminated
        {
            get { return _terminated; }
        }

        protected void MarkFrame()
        {
            _lastProcessedFrame = GetFrame();
        }

        protected void MarkInitialized()
        {
            _initialized = true;
        }

        protected void MarkUninitialized()
        {
            _initialized = false;
        }

        protected void MarkTerminated()
        {
            _terminated = true;
        }

        public override void OnCreated(IThreading threading)
        {
            _initialized = false;
            _terminated = false;

            base.OnCreated(threading);

            ModLogger.Debug(string.Format("{0} created", _monitorType));
        }

        public override void OnReleased()
        {
            _initialized = false;
            _terminated = false;

            base.OnReleased();

            ModLogger.Debug(string.Format("{0} released", _monitorType));
        }

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            // Exit if the monitor was terminated because of an error occured when updating overwatch data
            if (_terminated) return;

            // Exit if the monitor has not been loaded yet by the overwatch loader when a game is loaded (prevents issues when the loader is not started yet but the monitor is)
            if (!OverwatchControl.Instance.GameLoaded) return;

            try
            {
                // Initialize the monitor if it wasn't initialized yet
                if (!_initialized)
                    InitializeMonitor();
                // Run an update cycle if the simulation is not currently paused
                else if (!SimulationManager.instance.SimulationPaused)
                    RunUpdateCycle();
            }
            catch (Exception ex)
            {
                ModLogger.Error(string.Format("An unexpected error occured while updating the {0}", _monitorType));
                ModLogger.Exception(ex);
                _terminated = true;
            }

            base.OnUpdate(realTimeDelta, simulationTimeDelta);
        }

        private void RunUpdateCycle()
        {
            try
            {
                // Get a reference to the current frame
                int end = GetFrame();

                // Process all frames between the previous frame marker and the current frame
                while (_lastProcessedFrame != end)
                {
                    _lastProcessedFrame = GetFrame(_lastProcessedFrame + 1);

                    int[] boundaries = GetFrameBoundaries(_lastProcessedFrame);
                    
                    for (int i = boundaries[0]; i <= boundaries[1]; i++)
                    {
                        
                        CheckAssetExistence((ushort)i);
                    }
                }
            }
            catch (Exception ex)
            {
                ModLogger.Error("An unexpected error occured while running an update cycle in the building monitor");
                ModLogger.Exception(ex);
                MarkTerminated();
            }
        }

        
        protected int GetFrame()
        {
            return GetFrame((int)Singleton<SimulationManager>.instance.m_currentFrameIndex);
        }

        private int GetFrame(int index)
        {
            return (int)(index & 255);
        }

        private int[] GetFrameBoundaries(int index)
        {
            int frame = (int)(index & 255);
            int frame_first = frame * 128;
            int frame_last = (frame + 1) * 128 - 1;

            return new int[2] { frame_first, frame_last };
        }
    }
}
