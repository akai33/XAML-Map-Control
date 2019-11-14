﻿// XAML Map Control - https://github.com/ClemensFischer/XAML-Map-Control
// © 2019 Clemens Fischer
// Licensed under the Microsoft Public License (Ms-PL)

using System;
using System.Threading.Tasks;
#if WINDOWS_UWP
using Windows.UI.Xaml.Media;
#else
using System.Windows.Media;
#endif

namespace MapControl.MBTiles
{
    public sealed class MBTileSource : TileSource, IDisposable
    {
        public MBTileData TileData { get; }

        public MBTileSource(MBTileData tiledata)
        {
            TileData = tiledata;
        }

        public void Dispose()
        {
            TileData.Dispose();
        }

        public override async Task<ImageSource> LoadImageAsync(int x, int y, int zoomLevel)
        {
            var buffer = await TileData.ReadImageBufferAsync(x, y, zoomLevel);

            return buffer != null ? await ImageLoader.LoadImageAsync(buffer) : null;
        }
    }
}
