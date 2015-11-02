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

using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    /// <summary>
    /// Represents a CS loading extensions responsible for triggering the building monitor using the overwatch control flags
    /// </summary>
    public class OverwatchLoader : LoadingExtensionBase
    {
        /// <summary>
        /// Called when the overwatch loader is created
        /// </summary>
        /// <param name="loading">The loading instance</param>
        public override void OnCreated(ILoading loading)
        {
            ModLogger.Debug("OverwatchLoader created");
        }

        /// <summary>
        /// Called when the overwatch loader is released
        /// </summary>
        public override void OnReleased()
        {
            ModLogger.Debug("OverwatchLoader Released");
        }

        /// <summary>
        /// Called when a new or existing game is loaded, triggers the overwatch monitor by flagging the overwatch control
        /// </summary>
        /// <param name="mode">The mode.</param>
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode == LoadMode.NewGame || mode == LoadMode.LoadGame)
                OverwatchControl.Instance.GameLoaded = true;
        }

        /// <summary>
        /// Called when the game is being unloaded, cancels the overwatch monitof by flagging the overwatch control
        /// </summary>
        public override void OnLevelUnloading()
        {
            OverwatchControl.Instance.GameLoaded = false;
            OverwatchContainer.Instance.ClearCache();
        }
    }
}
