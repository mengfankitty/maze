using System;
using System.Collections.Generic;

namespace Maze.Common.Algorithms
{
    public class SidewinderMazeAlgorithm : IMazeUpdater
    {
        public void Update(Grid grid)
        {
            var rand = new Random();
            
            foreach (var row in grid.GetRows())
            {
                var run = new List<GridCell>();
                foreach (var cell in row)
                {
                    run.Add(cell);

                    var atEasternBoundary = cell.East == null;
                    var atNorthenBoundary = cell.North == null;

                    var shouldCloseOut = atEasternBoundary || (!atNorthenBoundary && rand.Next(2) == 0);

                    if (shouldCloseOut)
                    {
                        var member = run[rand.Next(run.Count)];
                        if (member.North != null)
                        {
                            member.Link(member.North);
                        }
                        
                        run.Clear();
                    }
                    else
                    {
                        cell.Link(cell.East);
                    }
                }
            }
        }
    }
}