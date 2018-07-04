using System;
using System.Linq;

namespace Maze.Common.Algorithms
{
    public class AldosBroderMazeAlgorithm : IMazeUpdater
    {
        public void Update(Grid grid)
        {
            var rand = new Random();
            var cell = grid.GetRandomCell();
            var unvisited = grid.Size - 1;

            while (unvisited > 0)
            {
                var neighbors = cell.Neighbors;
                var neighbor = neighbors[rand.Next(neighbors.Count)];
                if (!neighbor.Links.Any())
                {
                    cell.Link(neighbor);
                    --unvisited;
                }

                cell = neighbor;
            }
        }
    }
}