using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze.Common.Algorithms
{
    public class BinaryTreeMazeAlgorithm : IMazeUpdater
    {
        public void Update(Grid grid)
        {
            var random = new Random();

            foreach (var cell in grid.GetCells())
            {
                var neighbors = new List<GridCell>();
                if (cell.North != null) { neighbors.Add(cell.North);}
                if (cell.East != null) { neighbors.Add(cell.East); }

                if (!neighbors.Any()) continue;

                var selectedIndexToLink = random.Next(neighbors.Count);
                cell.Link(neighbors[selectedIndexToLink]);
            }
        }
    }
}