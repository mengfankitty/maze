using System.Collections.Generic;
using System.Linq;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class ColorLevelWriter : Writer
    {
        protected override IEnumerable<AreaRenderer> BackgroundRenderers => new[]
        {
            (AreaRenderer) new AreaColorRender(Rgba32.Black)
        };

        protected override IEnumerable<CellRenderer> GroundRenderers => Enumerable.Empty<CellRenderer>();

        protected override IEnumerable<CellRenderer> WallRenderers => new []
        {
            (CellRenderer) new CellColorRender(Rgba32.LightSkyBlue)
        };

        protected override GameLevelRenderSettings Settings => new GameLevelRenderSettings(4, 10);
    }
}
