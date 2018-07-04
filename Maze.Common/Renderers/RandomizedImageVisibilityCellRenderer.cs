using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.Primitives;

namespace Maze.Common.Renderers
{
    public class RandomizedImageVisibilityCellRenderer : CellRenderer
    {
        readonly bool _disposeTexturesWhenClose;
        readonly DisposableCollection<Image<Rgba32>> _textures;
        readonly Random _random = new Random();
        bool _isDisposed;

        protected virtual bool IsSupported(RenderCell cell) => true;
        public int Possibility { get; set; } = 30;

        public RandomizedImageVisibilityCellRenderer(
            IEnumerable<Image<Rgba32>> textures, 
            bool disposeTexturesWhenClose)
        {
            _disposeTexturesWhenClose = disposeTexturesWhenClose;
            _textures = new DisposableCollection<Image<Rgba32>>(textures);
        }
        
        public override void Render(IImageProcessingContext<Rgba32> context, Rectangle cellArea, RenderCell cell)
        {
            if (!IsSupported(cell)) return;
            var shouldRenderer = _random.Next(100) <= Possibility;
            if (shouldRenderer)
            {
                var texture = _textures[_random.Next(_textures.Count)];
                var maxX = cellArea.Width - texture.Width;
                var maxY = cellArea.Height - texture.Height;
                
                // Will not renderer if texture is too large
                if (maxX < 0 || maxY < 0) return;

                context.DrawImage(
                    texture, 
                    1.0f, 
                    new Point(cellArea.Left + _random.Next(maxX), cellArea.Top + _random.Next(maxY)));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing && _disposeTexturesWhenClose)
            {
                _textures.Dispose();
            }

            _isDisposed = true;
        }
    }
}