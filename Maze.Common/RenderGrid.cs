﻿using System.Collections.Generic;
using System.Linq;

namespace Maze.Common
{
    public class RenderGrid
    {
        public int RowCount { get; }
        public int ColumnCount { get; }
        readonly RenderCell[][] _renderGrid;
        
        public RenderGrid(Grid grid)
        {
            RowCount = TranslateGridIndex(grid.RowCount);
            ColumnCount = TranslateGridIndex(grid.ColumnCount);
            _renderGrid = AllocateGrid();
            PrepareGrid(grid);
        }

        static int TranslateGridIndex(int index)
        {
            return 1 + index * 2;
        }

        void PrepareGrid(Grid grid)
        {
            ConfigureNeighbors();
            ConfigureCellTypes(grid);
        }

        void ConfigureCellTypes(Grid grid)
        {
            SetWesternWall(_renderGrid);
            SetNorthenWall(_renderGrid);
            SetCellAndLinkedPart(_renderGrid, grid);
            RemoveIsolatedConnector(_renderGrid);
            SetDirection(_renderGrid);
        }

        static void SetDirection(RenderCell[][] renderGrid)
        {
            foreach (var rows in renderGrid)
            {
                foreach (var cell in rows)
                {
                    var selfType = cell.RenderType;
                    if (cell.East?.RenderType == selfType) cell.Direction |= Direction.East;
                    if (cell.West?.RenderType == selfType) cell.Direction |= Direction.West;
                    if (cell.North?.RenderType == selfType) cell.Direction |= Direction.North;
                    if (cell.South?.RenderType == selfType) cell.Direction |= Direction.South;
                }
            }
        }

        void RemoveIsolatedConnector(RenderCell[][] renderGrid)
        {
            for (var rowIndex = 2; rowIndex < RowCount - 1; rowIndex += 2)
            {
                for (var columnIndex = 2; columnIndex < ColumnCount - 1; columnIndex += 2)
                {
                    var connector = renderGrid[rowIndex][columnIndex];
                    if (connector.Neighbors.All(n => n.RenderType != RenderType.Wall))
                    {
                        connector.RenderType = RenderType.Ground;
                    }
                }
            }
        }

        static void SetCellAndLinkedPart(RenderCell[][] renderGrid, Grid grid)
        {
            foreach (var cell in grid.GetCells())
            {
                var centerRowIndex = TranslateGridIndex(cell.Row);
                var centerColumnIndex = TranslateGridIndex(cell.Column);
                var center = renderGrid[centerRowIndex][centerColumnIndex];
                center.RenderType = RenderType.Ground;
                renderGrid[centerRowIndex + 1][centerColumnIndex + 1].RenderType = RenderType.Wall;
                renderGrid[centerRowIndex][centerColumnIndex + 1].RenderType =
                    cell.IsLinked(cell.East) ? RenderType.Ground : RenderType.Wall;
                renderGrid[centerRowIndex + 1][centerColumnIndex].RenderType =
                    cell.IsLinked(cell.South) ? RenderType.Ground : RenderType.Wall;
            }
        }

        void SetNorthenWall(RenderCell[][] renderGrid)
        {
            for (var columnIndex = 0; columnIndex < ColumnCount; ++columnIndex)
            {
                renderGrid[0][columnIndex].RenderType = RenderType.Wall;
            }
        }

        void SetWesternWall(RenderCell[][] renderGrid)
        {
            for (var rowIndex = 0; rowIndex < RowCount; ++rowIndex)
            {
                renderGrid[rowIndex][0].RenderType = RenderType.Wall;
            }
        }

        void ConfigureNeighbors()
        {
            foreach (var rows in _renderGrid)
            {
                foreach (var cell in rows)
                {
                    var cellRow = cell.Row;
                    var cellColumn = cell.Column;
                    cell.SetNeighbors(
                        this[cellRow - 1, cellColumn],
                        this[cellRow + 1, cellColumn],
                        this[cellRow, cellColumn + 1],
                        this[cellRow, cellColumn - 1]);
                }
            }
        }

        RenderCell[][] AllocateGrid()
        {
            var theGrid = new RenderCell[RowCount][];
            for (var rowIndex = 0; rowIndex < RowCount; ++rowIndex)
            {
                var currentRow = new RenderCell[ColumnCount];
                for (var columnIndex = 0; columnIndex < ColumnCount; ++columnIndex)
                {
                    currentRow[columnIndex] = new RenderCell(rowIndex, columnIndex);
                }

                theGrid[rowIndex] = currentRow;
            }

            return theGrid;
        }
        
        public RenderCell this[int rowIndex, int columnIndex]
        {
            get
            {
                if (rowIndex < 0 || rowIndex >= RowCount) return null;
                if (columnIndex < 0 || columnIndex >= ColumnCount) return null;
                return _renderGrid[rowIndex][columnIndex];
            }
        }

        public IEnumerable<RenderCell> GetCells()
        {
            foreach (var rows in _renderGrid)
            {
                foreach (var cell in rows)
                {
                    yield return cell;
                }
            }
        }
    }
}