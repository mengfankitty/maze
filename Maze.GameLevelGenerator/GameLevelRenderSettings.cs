﻿namespace Maze.GameLevelGenerator
{
    public class GameLevelRenderSettings
    {
        public GameLevelRenderSettings(int cellSize, int margin)
        {
            CellSize = cellSize;
            Margin = margin;
        }

        public int CellSize { get; }
        public int Margin { get; }
    }
}