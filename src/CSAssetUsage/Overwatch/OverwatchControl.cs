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
    public class OverwatchControl
    {
        private static readonly OverwatchControl _instance = new OverwatchControl();

        private bool _buildingMonitorSpun;

        public OverwatchControl()
        {
            GameLoaded = false;
            _buildingMonitorSpun = false;
        }

        public static OverwatchControl Instance
        {
            get { return _instance; }
        }

        public bool BuildingMonitorSpun
        {
            get { return BuildingMonitorSpinnable && _buildingMonitorSpun; }
            set { _buildingMonitorSpun = BuildingMonitorSpinnable ? value : false; }
        }

        public bool GameLoaded { get; set; }

        public bool BuildingMonitorSpinnable
        {
            get { return GameLoaded; }
        }
    }
}
