using System;
using System.Collections.Generic;
using System.Reflection;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class CityLevelWriter : Writer
    {
        protected override IEnumerable<AreaRenderer> BackgroundRenderers => new List<AreaRenderer>
        {
            new AreaColorRender(Rgba32.White)
        };

        protected override IEnumerable<CellRenderer> GroundRenderers => Array.Empty<CellRenderer>();

        protected override IEnumerable<CellRenderer> WallRenderers => new List<CellRenderer>
        {
            new RandomizedImageCellRenderer(
                Assembly.GetExecutingAssembly().LoadEmbeddedResources(
                    new[]
                    {
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_1.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_2.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_3.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_4.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_5.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_6.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_7.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_8.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_9.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_10.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_wall_11.png"
                    }),
                true)
        };

        protected override GameLevelRenderSettings Settings => new GameLevelRenderSettings(200, 100);
    }
}
