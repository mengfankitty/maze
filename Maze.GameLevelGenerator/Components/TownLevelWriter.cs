using System.Collections.Generic;
using Maze.Common.Renderers;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator.Components
{
    public class TownLevelWriter : Writer
    {
        protected override IEnumerable<AreaRenderer> BackgroundRenderers => new []
        {
            new AreaColorRender(Rgba32.White)
        };

        protected override IEnumerable<CellRenderer> GroundRenderers => new List<CellRenderer>
        {
            new DirectedCellRenderer(
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_south.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_south.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_south.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_south_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_south_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_south_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_south_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_north_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_south_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.road_all.png"))
        };

        protected override IEnumerable<CellRenderer> WallRenderers => new List<CellRenderer>
        {
            new RandomizedImageCellRenderer(
                Assembly.LoadEmbeddedResources(
                    new[]
                    {
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_1.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_2.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_3.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_4.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_5.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_6.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_7.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_8.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_9.png",
                        "Maze.GameLevelGenerator.Textures.fake_3d_sm_wall_10.png"
                    }),
                true)
        };

        protected override GameLevelRenderSettings Settings => new GameLevelRenderSettings(100, 50);
    }
}
