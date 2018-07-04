using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Maze.Common.Renderers;

namespace Maze.GameLevelGenerator.Components
{
    public class GrassLevelWriter : IWriter
    {
        public void Write(Stream stream, MazeGridSettings mazeSettings)
        {
            var renderGrid = new MazeGridFactory(mazeSettings).CreateRenderGrid();
            var renderer = new NormalGameLevelRenderer(
                CreateGrassBackground(),
                CreateGrassWithDirts(),
                CreateRailings(),
                new GameLevelRenderSettings(32, 20));
            using (renderer)
            {
                renderer.Render(renderGrid, stream);
            }
        }

        static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

        IEnumerable<AreaRenderer> CreateGrassBackground()
        {
            var resource = Assembly.GetExecutingAssembly().LoadEmbeddedResource(
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_1.png");
            yield return new AreaTileImageRenderer(resource, true);
        }

        IEnumerable<CellRenderer> CreateGrassWithDirts()
        {
            yield return CreateGrassGroundRenderer();
            yield return CreateGrassDirtRenderer();
        }

        static CellRenderer CreateGrassGroundRenderer()
        {
            string[] resourceKeys =
            {
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_1.png",
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_2.png",
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_3.png",
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_4.png",
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_5.png",
                "Maze.GameLevelGenerator.Textures.sm_grass_ground_6.png"
            };

            return new RandomizedImageCellRenderer(
                Assembly.LoadEmbeddedResources(resourceKeys),
                true);
        }

        static CellRenderer CreateGrassDirtRenderer()
        {
            return new DirectedCellRenderer(
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
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.grass_sm_dart_north_south_east_west.png"));
        }

        IEnumerable<CellRenderer> CreateRailings()
        {
            var east =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_east.png");
            var eastWest =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_east_west.png");
            var north =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north.png");
            var northEast =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_east.png");
            var northSouth =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_south.png");
            var northWest =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_north_west.png");
            var south =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south.png");
            var southEast =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east.png");
            var southEastWest =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_east_west.png");
            var southWest =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_south_west.png");
            var west =
                Assembly.LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.sm_grass_wall_west.png");
            yield return new DirectedCellRenderer(
                north,
                south,
                east,
                west,
                northSouth,
                northEast,
                northWest,
                southEast,
                southWest,
                eastWest,
                southEast,
                southWest,
                eastWest,
                southEastWest,
                southEastWest);
        }
    }
}
