using System.Collections.Generic;
using System.Reflection;
using Maze.Common.Renderers;

namespace Maze.GameLevelGenerator.Components
{
    public class TreeLevelWriter : Writer
    {
        protected override IEnumerable<AreaRenderer> BackgroundRenderers => new List<AreaRenderer>
        {
            new AreaTileImageRenderer(Assembly.GetExecutingAssembly()
                    .LoadEmbeddedResource("Maze.GameLevelGenerator.Textures.lovely_tree_ground.png"),
                true)
        };

        protected override IEnumerable<CellRenderer> GroundRenderers => new List<CellRenderer>
        {
            new RandomizedImageVisibilityCellRenderer(
                Assembly.GetExecutingAssembly().LoadEmbeddedResources(new[]
                {
                    "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_1.png",
                    "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_2.png",
                    "Maze.GameLevelGenerator.Textures.lovely_tree_accessory_3.png"
                }),
                true)
        };

        protected override IEnumerable<CellRenderer> WallRenderers => new List<CellRenderer>
        {
            new RandomizedImageCellRenderer(
                Assembly.GetExecutingAssembly().LoadEmbeddedResources(new[]
                {
                    "Maze.GameLevelGenerator.Textures.lovely_tree_wall_1.png",
                    "Maze.GameLevelGenerator.Textures.lovely_tree_wall_2.png",
                    "Maze.GameLevelGenerator.Textures.lovely_tree_wall_3.png"
                }),
                true)
        };

        protected override GameLevelRenderSettings Settings => new GameLevelRenderSettings(78, 20);
    }
}
