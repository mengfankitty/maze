using System.Collections.Generic;
using System.Reflection;
using Maze.Common.Renderers;

namespace Maze.GameLevelGenerator.Components
{
    public class GrassLevelWriter : Writer
    {
        protected override IEnumerable<AreaRenderer> BackgroundRenderers => new List<AreaRenderer>
        {
            new AreaTileImageRenderer(Assembly.GetExecutingAssembly()
                    .LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_ground_1.png"),
                true)
        };

        protected override IEnumerable<CellRenderer> GroundRenderers => new List<CellRenderer>
        {
            new RandomizedImageCellRenderer(
                Assembly.LoadEmbeddedResources(new[] {
                    "Maze.GameLevelGenerator.Textures.sm_grass_ground_1.png",
                    "Maze.GameLevelGenerator.Textures.sm_grass_ground_2.png",
                    "Maze.GameLevelGenerator.Textures.sm_grass_ground_3.png",
                    "Maze.GameLevelGenerator.Textures.sm_grass_ground_4.png",
                    "Maze.GameLevelGenerator.Textures.sm_grass_ground_5.png",
                    "Maze.GameLevelGenerator.Textures.sm_grass_ground_6.png"
                }),
                true),
            new DirectedCellRenderer(
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_south_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south_east_west.png"))
        };

        protected override IEnumerable<CellRenderer> WallRenderers => new List<CellRenderer>
        {
            new DirectedCellRenderer(
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_south.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east_west.png"),
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east_west.png"))
        };

        protected override GameLevelRenderSettings Settings => new GameLevelRenderSettings(32, 20);
    }
}
