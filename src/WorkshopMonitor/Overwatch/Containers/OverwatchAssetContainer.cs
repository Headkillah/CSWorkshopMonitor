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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Overwatch
{
    public abstract class OverwatchAssetContainer<TAssetWrapperType, TCSAsset>
        where TAssetWrapperType : IOverwatchAssetWrapper
    {
        private Dictionary<ushort, IOverwatchAssetWrapper> _assetCache;
        private List<IOverwatchAssetWrapper> _childAssetCache;

        protected abstract TAssetWrapperType CreateAssetWrapper(ushort assetId, TCSAsset asset);

        /// <summary>
        /// Prevents a default instance of the <see cref="OverwatchAssetContainer"/> class from being created.
        /// </summary>
        public OverwatchAssetContainer()
        {
            _assetCache = new Dictionary<ushort, IOverwatchAssetWrapper>();
            _childAssetCache = new List<IOverwatchAssetWrapper>();
        }

        public bool HasAsset(ushort assetId)
        {
            return _assetCache.ContainsKey(assetId);
        }

        public virtual void CacheAsset(ushort assetId, TCSAsset asset)
        {
            var overwatchAsset = CreateAssetWrapper(assetId, asset);
            _assetCache.Add(assetId, overwatchAsset);
        }

        protected internal virtual void CacheChildAsset(IOverwatchAssetWrapper childAssetWrapper)
        {
            _childAssetCache.Add(childAssetWrapper);
        }

        public void ClearCache()
        {
            _assetCache.Clear();
        }

        public void RemoveAsset(ushort id)
        {
            if (_assetCache.ContainsKey(id))
                _assetCache.Remove(id);
            foreach(var child in _childAssetCache.Where(c => c.ParentAssetId == id).ToList())
            {
                _childAssetCache.Remove(child);
            }
        }

        public int GetAssetCount(string technicalName)
        {
            var assetCount = _assetCache.Values.Count(a => a.TechnicalName == technicalName);
            var childAssetCount = _childAssetCache.Count(ca => ca.TechnicalName == technicalName);
            return assetCount + childAssetCount;
        }
    }
}
