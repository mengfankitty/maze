﻿using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.Primitives;

namespace Maze.Common.Renderers
{
    public class RandomizedImageCellRenderer : CellRenderer
    {
        readonly bool _disposeTextureWhenClose;
        readonly DisposableCollection<Image<Rgba32>> _textures;
        readonly Random _random = new Random();
        bool _isDisposed;

        protected virtual bool IsSupported(RenderCell cell) => true;

        public RandomizedImageCellRenderer(
            IEnumerable<Image<Rgba32>> textures,
            bool disposeTexturesWhenClose)
        {
            _disposeTextureWhenClose = disposeTexturesWhenClose;
            _textures = new DisposableCollection<Image<Rgba32>>(textures);
        }

        public override void Render(IImageProcessingContext<Rgba32> context, Rectangle cellArea, RenderCell cell)
        {
            if (!IsSupported(cell)) return;
            var texture = _textures[_random.Next(_textures.Count)];
            var location = new Point(
                (cellArea.Left + cellArea.Right) / 2,
                cellArea.Bottom);
            location.X -= texture.Width / 2;
            location.Y -= texture.Height;
            context.DrawImage(texture, PixelBlenderMode.Normal, 1.0f, location);
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing && _disposeTextureWhenClose)
            {
                _textures.Dispose();
            }

            _isDisposed = true;
        }
    }
}