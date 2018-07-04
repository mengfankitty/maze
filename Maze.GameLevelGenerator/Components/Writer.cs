using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Maze.Common.Renderers;

namespace Maze.GameLevelGenerator.Components
{
    public abstract class Writer : IWriter
    {
        public void Write(Stream stream, MazeGridSettings settings)
        {
            var grid = new MazeGridFactory(settings).CreateRenderGrid();
            var renderer = new NormalGameLevelRenderer(
                BackgroundRenderers,
                GroundRenderers,
                WallRenderers,
                Settings
            );
            using (renderer)
            {
                renderer.Render(grid, stream);
            }
        }

        protected static Assembly Assembly => Assembly.GetExecutingAssembly();

        protected abstract IEnumerable<AreaRenderer> BackgroundRenderers { get; }

        protected abstract IEnumerable<CellRenderer> GroundRenderers { get; }

        protected abstract IEnumerable<CellRenderer> WallRenderers { get; }

        protected abstract GameLevelRenderSettings Settings { get; }
    }
}
