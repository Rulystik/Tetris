using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
  public class TetraminoData
  {
        
    public Action NewPosition;

    private Cell[,] cell = new Cell[4, 4];
    private Cell[,] cellCache = new Cell[4, 4];

    public ShiftDirection Direction { get; private set; }

    public void Init(bool[,] array)
    {
      int a = 3;
      int b = 16;
      for (int x = 0; x < cell.GetLongLength(0); x++)
      for (int y = 0; y < cell.GetLongLength(1); y++)
      {
        cell[x,y] = new Cell{X = a+x, Y = b+y , init = array[x,y]};
      }
      cellCache = (Cell[,]) cell.Clone();
      NewPosition?.Invoke();
    }

    public Cell[,] Cell => cell;
    public Cell[,] CellCache => cellCache;


    public void Shift(ShiftDirection dir)
    {
      Direction = dir;
      cellCache = (Cell[,]) cell.Clone();
      
      if (Direction == ShiftDirection.rotate)
        Rotate();
      else
        Shift();
    }

    private void Rotate()
    {
      cell = RotateCell(cell);
      NewPosition?.Invoke();
    }


    // public void ShiftLeft()
    // {
    //   Direction = ShiftDirection.left;
    //   Shift();
    // }
    //
    // public void ShiftRight()
    // {
    //   Direction = ShiftDirection.right;
    //   Shift();
    // }
        
    public void StepBack()
    {
      cell = (Cell[,]) cellCache.Clone();
    }
        
    private Cell[,] RotateCell(Cell[,] target)
    {
      bool[,] figure = new bool[4,4];
            
      for (int x = 0; x < target.GetLongLength(0); x++)
      for (int y = 0; y < target.GetLongLength(1); y++)
      {
        figure[x, y] = target[x, y].init;
      }

      figure = RotateMatrix(figure);
            
      for (int x = 0; x < target.GetLongLength(0); x++)
      for (int y = 0; y < target.GetLongLength(1); y++)
      {
        target[x, y].init = figure[x, y];
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
}