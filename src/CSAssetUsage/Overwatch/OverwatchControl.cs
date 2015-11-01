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
    /// Represents a class repsonsible for controlling the building monitor
    /// </summary>
    public class OverwatchControl
    {
        private static readonly OverwatchControl _instance = new OverwatchControl();

        private bool _buildingMonitorSpun;
        private bool _gameLoaded;


        /// <summary>
        /// Initializes a new instance of the <see cref="OverwatchControl"/> class
        /// </summary>
        private OverwatchControl()
        {
            _gameLoaded = false;
            _buildingMonitorSpun = false;
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="OverwatchControl"/> class
        /// </summary>
        public static OverwatchControl Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets or sets a flag indicating whether the building monitor is spinning
        /// </summary>
        public bool BuildingMonitorSpun
        {
            get { return GameLoaded && _buildingMonitorSpun; }
            set { _buildingMonitorSpun = GameLoaded ? value : false; }
        }

        /// <summary>
        /// Gets or sets a flag indicating whether a game has been loaded
        /// </summary>
        public bool GameLoaded
        {
            get { return _gameLoaded; }
            set
            {
                _gameLoaded = value;
                if (value)
                    ModLogger.Debug("Game is marked as loaded by overwatch control");
                else
                    ModLogger.Debug("Game is marked as unloaded by overwatch control");
            }
        }
    }
}
