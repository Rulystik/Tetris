using System;
using System.Collections.Generic;
using System.Linq;

public class TetraminoData
{
  public Action NewPosition;
  public Action NewFigure;

  private Cell[,] cell = new Cell[4, 4];
  private Cell[,] cellCache = null;

  public Cell[,] Cell => cell;
  public Cell[,] CellCache => cellCache;
  public ShiftDirection Direction { get; private set; }


  public List<Cell> GetNewExtractCache()
  {
    List<Cell> result = new List<Cell>();
      
    for (int x = 0; x < Cell.GetLength(0); x++)
    for (int y = 0; y < Cell.GetLength(1); y++)
    {
      if (Cell[x,y].Value == true)
      {
        result.Add(new Cell()
        {
          X = cell[x,y].X,
          Y = cell[x,y].Y,
          Value = cell[x,y].Value
        });
      }
    }

    if (cellCache == null)
    {
      return result;
    }
      
    for (int x = 0; x < cellCache.GetLength(0); x++)
    for (int y = 0; y < cellCache.GetLength(1); y++)
    {
      if (cellCache[x, y].Value == true)
      {
        for (int i = 0; i < result.Count; i++)
        {
          if (result[i].X == cellCache[x, y].X && result[i].Y == cellCache[x, y].Y)
          {
            result.Remove(result[i]);
          }
        }
      }
    }

    return result;
  }
    
  public void CreateNew (bool[,] array)
  {
    for (int x = 0; x < cell.GetLongLength(0); x++)
    for (int y = 0; y < cell.GetLongLength(1); y++)
    {
      cell[x,y] = new Cell{X = Model.startPos[0] + x, Y = Model.startPos[1] + y , Value = array[x,y]};
    }

    cellCache = null;
    NewFigure?.Invoke();
  }

  public void Shift(ShiftDirection dir)
  {
    Direction = dir;
    cellCache = (Cell[,]) cell.Clone();
      
    if (Direction == ShiftDirection.rotate)
      Rotate();
    else
      Shift();
  }
    
  public bool TryStepBack()
  {
    if (cellCache == null)
    {
      return false;
    }
    cell = (Cell[,]) cellCache.Clone();
    return true;
  }

  private void Rotate()
  {
    cell = RotateCell(cell);
    NewPosition?.Invoke();
  }

  private Cell[,] RotateCell(Cell[,] target)
  {
    bool[,] figure = new bool[4,4];
            
    for (int x = 0; x < target.GetLongLength(0); x++)
    for (int y = 0; y < target.GetLongLength(1); y++)
    {
      figure[x, y] = target[x, y].Value;
    }

    figure = RotateMatrix(figure);
            
    for (int x = 0; x < target.GetLongLength(0); x++)
    for (int y = 0; y < target.GetLongLength(1); y++)
    {
      target[x, y].Value = figure[x, y];
    }

    return target;
  }

  private void Shift()
  {
    for (int x = 0; x < cell.GetLongLength(0); x++)
    for (int y = 0; y < cell.GetLongLength(1); y++)
    {
      if(Direction == ShiftDirection.down)
        cell[x, y].Y -= 1;
      else
        cell[x, y].X += Direction == ShiftDirection.left? -1 : 1;
    }
      
    NewPosition?.Invoke();
  }

  private bool[,] RotateMatrix(bool[,] matrix) {
    bool[,] ret = new bool[4,4];

    for (int i = 0; i < 4; ++i) {
      for (int j = 0; j <4; ++j) {
        ret[i, j] = matrix[4 - j - 1, i];
      }
    }

    return ret;
  }

}